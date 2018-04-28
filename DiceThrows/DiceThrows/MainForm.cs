using DiceThrows.EntityFramework.Database;
using DiceThrows.EntityFramework.DataModel;
using DiceThrows.EntityFramework.Service;
using DiceThrows.FileStoreFactory;
using DiceThrows.Utils;
using SingletonLogger.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DiceThrows
{
    public partial class MainForm : Form
    {
        private Thread _rollBlackDiceThread;
        private Thread _rollWhiteDiceThread;
        readonly ManualResetEvent _stopThread_RollBlackDice;
        readonly ManualResetEvent _thread_RollBlackDice_Stopped;
        readonly ManualResetEvent _stopThread_RollWhiteDice;
        readonly ManualResetEvent _thread_RollWhiteDice_Stopped;
        bool _rollBlackDiceThreadAborted = false;
        bool _rollBlackDiceThreadStopped = false;
        bool _rollWhiteDiceThreadAborted = false;
        bool _rollWhiteDiceThreadStopped = false;
        bool _transactionDisposed = false;
        DbContextTransaction _transaction = null;
        Random _rndBlack, _rndWhite;
        int _blackAcesCounter = 0;
        int _whiteAcesCounter = 0;
        int _blackThrowsCounter = 0;
        int _whiteThrowsCounter = 0;
        public static Mutex Mut = new Mutex();
        public MainForm()
        {
            InitializeComponent();
            //Make sure directory exists
            FilesOperations.PrepareFolders();
            // Set |DataDirectory| value
            AppDomain.CurrentDomain.SetData("DataDirectory", "C:\\Users\\Public\\Documents\\DiceThrows\\");
            this._stopThread_RollBlackDice = new ManualResetEvent(false);
            this._thread_RollBlackDice_Stopped = new ManualResetEvent(false);
            this._stopThread_RollWhiteDice = new ManualResetEvent(false);
            this._thread_RollWhiteDice_Stopped = new ManualResetEvent(false);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!_rollBlackDiceThreadAborted && !_rollBlackDiceThreadStopped) AbortThread_RollBlackDice();
                if (!_rollWhiteDiceThreadAborted && !_rollWhiteDiceThreadStopped) AbortThread_RollWhiteDice();

                string _DataFile = String.Concat(DiceThrows.Utils.FilesOperations.GetAllUsersPublicFolder(), "\\Documents\\" + "\\DiceThrows\\Data", "\\dicethrows.db");
                if (System.IO.File.Exists(_DataFile))
                {
                    //Compact SQLite DB
                    (new DB()).Compact();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.LogMsg(ex.Message, Log.Level.Error);
            }
            finally
            {
                Log.Instance.Terminate();
            }
        }

        private void btn_create_dices_Click(object sender, EventArgs e)
        {

            var db = new DB();

            if (db.Dices.Count() != 0)  return;

            Dice _blackDice = new Dice { Color = DiceColor.Black };
            Dice _whiteDice = new Dice { Color = DiceColor.White };
            db.Dices.Add(_blackDice);
            db.SaveChanges();
            Debug.WriteLine(_blackDice.ToString());
            db.Dices.Add(_whiteDice);
            db.SaveChanges();
            Debug.WriteLine(_whiteDice.ToString());
        }

        private void btn_roll_dices_Click(object sender, EventArgs e)
        {
            var db = new DB();

            if (db.Dices.Count() == 2)
            {
                if ((this._rollBlackDiceThread != null) && (this._rollBlackDiceThread.IsAlive))
                {
                    //Abort running thread
                    this.AbortThread_RollBlackDice();
                }
                if ((this._rollWhiteDiceThread != null) && (this._rollWhiteDiceThread.IsAlive))
                {
                    //Abort running thread
                    this.AbortThread_RollWhiteDice();
                }
                _rollBlackDiceThreadAborted = false;
                _rollBlackDiceThreadStopped = false;
                _rollWhiteDiceThreadAborted = false;
                _rollWhiteDiceThreadStopped = false;
                _transactionDisposed = false;


                var blackDice = (from dice in db.Dices
                                 where dice.Color == DiceColor.Black
                                 select dice).FirstOrDefault();
                var whiteDice = (from dice in db.Dices
                                 where dice.Color == DiceColor.White
                                 select dice).FirstOrDefault();

                //RollResultService rollResultService = new RollResultService(db);
                //m_BlackAcesCounter = rollResultService.GetNumberOfAces(DiceColor.Black);
                //m_WhiteAcesCounter = rollResultService.GetNumberOfAces(DiceColor.White);
                ////m_BlackAcesCounter = db.RollResults.Where(rollResult => rollResult.Dice.Color == DiceColor.Black && rollResult.RollValue == 1).Count();
                ////m_WhiteAcesCounter = db.RollResults.Where(rollResult => rollResult.Dice.Color == DiceColor.White && rollResult.RollValue == 1).Count();
                //lbl_black_dice_counter.Text = m_BlackAcesCounter.ToString();
                //lbl_white_dice_counter.Text = m_WhiteAcesCounter.ToString();

                _rndBlack = new Random(blackDice.GetHashCode());
                _rndWhite = new Random(whiteDice.GetHashCode());

                _transaction = db.Database.BeginTransaction();
                //start thread
                this._rollBlackDiceThread = new Thread(delegate () { this.RollBlackDice(_rndBlack, blackDice, db, _transaction); });
                this._stopThread_RollBlackDice.Reset();
                this._thread_RollBlackDice_Stopped.Reset();
                this._rollBlackDiceThread.Start();

                //start thread
                this._rollWhiteDiceThread = new Thread(delegate () { this.RollWhiteDice(_rndWhite, whiteDice, db, _transaction); });
                this._stopThread_RollWhiteDice.Reset();
                this._thread_RollWhiteDice_Stopped.Reset();
                this._rollWhiteDiceThread.Start();

                /*
                DiceService _diceService = new DiceService(db);
                List<RollResult> _rollResultList = new List<RollResult>();
                var transaction = db.Database.BeginTransaction();
                foreach (Dice dice in db.Dices)
                {
                    RollResult _rollResult = _diceService.RollDice(dice);
                    db.RollResults.Add(_rollResult);
                    db.SaveChanges();
                    Debug.WriteLine(_rollResult.ToString());
                    //_rollResultList.Add(_rollResult);
                }
                transaction.Commit();
                //db.RollResults.AddRange(_rollResultList);
                //db.SaveChanges();
                */
            }
        }
        public void AbortThread_RollBlackDice()
        {
            // waits until m_RollBlackDiceThread finishes
            if ((this._rollBlackDiceThread != null) && this._rollBlackDiceThread.IsAlive)
            {
                this._stopThread_RollBlackDice.Set();
                //m_RollBlackDiceThread.Abort();
                _rollBlackDiceThreadAborted = true;
                while (this._rollBlackDiceThread.IsAlive)
                {
                    if (WaitHandle.WaitAll(
                        (new ManualResetEvent[] { this._thread_RollBlackDice_Stopped }),
                        100, true))
                    {
                        break;
                    }
                    Application.DoEvents();
                }
            }
        }
        public void AbortThread_RollWhiteDice()
        {
            // waits until m_RollWhiteDiceThread finishes
            if ((this._rollWhiteDiceThread != null) && this._rollWhiteDiceThread.IsAlive)
            {
                this._stopThread_RollWhiteDice.Set();
                //m_RollWhiteDiceThread.Abort();
                _rollWhiteDiceThreadAborted = true;
                while (this._rollWhiteDiceThread.IsAlive)
                {
                    if (WaitHandle.WaitAll(
                        (new ManualResetEvent[] { this._thread_RollWhiteDice_Stopped }),
                        100, true))
                    {
                        break;
                    }
                    Application.DoEvents();
                }
            }
        }
        public void StopThread_RollBlackDice()
        {
            // waits until m_RollBlackDiceThread finishes
            if ((this._rollBlackDiceThread != null) && this._rollBlackDiceThread.IsAlive)
            {
                this._stopThread_RollBlackDice.Set();
                _rollBlackDiceThreadStopped = true;
                while (this._rollBlackDiceThread.IsAlive)
                {
                    if (WaitHandle.WaitAll(
                        (new ManualResetEvent[] { this._thread_RollBlackDice_Stopped }),
                        100, true))
                    {
                        break;
                    }
                    Application.DoEvents();
                }
            }
        }
        public void StopThread_RollWhiteDice()
        {
            // waits until m_RollWhiteDiceThread finishes
            if ((this._rollWhiteDiceThread != null) && this._rollWhiteDiceThread.IsAlive)
            {
                this._stopThread_RollWhiteDice.Set();
                _rollWhiteDiceThreadStopped = true;
                while (this._rollWhiteDiceThread.IsAlive)
                {
                    if (WaitHandle.WaitAll(
                        (new ManualResetEvent[] { this._thread_RollWhiteDice_Stopped }),
                        100, true))
                    {
                        break;
                    }
                    Application.DoEvents();
                }
            }
        }
        public void RollBlackDice(Random rnd, Dice dice, DB db, DbContextTransaction transaction)
        {
            try
            {
                DiceService _diceService = new DiceService(db);

                while (true && !_rollBlackDiceThreadAborted && !_rollBlackDiceThreadStopped)
                {
                    RollResult _rollResult = _diceService.RollDice(rnd, dice);
                    if (Mut.WaitOne())
                    {
                        try
                        {
                            if (!_transactionDisposed)
                            {
                                db.RollResults.Add(_rollResult);
                                db.SaveChanges();
                                if (_rollResult.RollValue == 1) _blackAcesCounter++;
                                _blackThrowsCounter++;
                                if (!lbl_black_dice_ace_counter.IsDisposed)
                                {
                                    lbl_black_dice_ace_counter.BeginInvoke((MethodInvoker)(() =>
                                    {
                                        lbl_black_dice_ace_counter.Text = _blackAcesCounter.ToString();
                                    }));
                                }
                                if (!lbl_black_dice_throws_counter.IsDisposed)
                                {
                                    lbl_black_dice_throws_counter.BeginInvoke((MethodInvoker)(() =>
                                    {
                                        lbl_black_dice_throws_counter.Text = _blackThrowsCounter.ToString();
                                    }));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            Mut.ReleaseMutex();
                        }
                    }
                    Debug.WriteLine(_rollResult.ToString());
                }
                if (_rollBlackDiceThreadStopped)
                {
                    if (Mut.WaitOne())
                    {
                        try
                        {
                            if (!_transactionDisposed)
                            {
                                transaction.Commit();
                                transaction.Dispose();
                                _transactionDisposed = true;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            Mut.ReleaseMutex();
                        }
                    }
                }
                if (_rollBlackDiceThreadAborted)
                {
                    if (Mut.WaitOne())
                    {
                        try
                        {
                            if (!_transactionDisposed)
                            {
                                transaction.Rollback();
                                transaction.Dispose();
                                _transactionDisposed = true;
                                ResetCounters(db);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            Mut.ReleaseMutex();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void RollWhiteDice(Random rnd, Dice dice, DB db, DbContextTransaction transaction)
        {
            try
            {
                DiceService _diceService = new DiceService(db);

                while (true && !_rollWhiteDiceThreadAborted && !_rollWhiteDiceThreadStopped)
                {
                    RollResult _rollResult = _diceService.RollDice(rnd, dice);
                    if (Mut.WaitOne())
                    {
                        try
                        {
                            if (!_transactionDisposed)
                            {
                                db.RollResults.Add(_rollResult);
                                db.SaveChanges();
                                if (_rollResult.RollValue == 1) _whiteAcesCounter++;
                                _whiteThrowsCounter++;
                                if (!lbl_white_dice_ace_counter.IsDisposed)
                                {
                                    lbl_white_dice_ace_counter.BeginInvoke((MethodInvoker)(() =>
                                    {
                                        lbl_white_dice_ace_counter.Text = _whiteAcesCounter.ToString();
                                    }));
                                }
                                if (!lbl_white_dice_throws_counter.IsDisposed)
                                {
                                    lbl_white_dice_throws_counter.BeginInvoke((MethodInvoker)(() =>
                                    {
                                        lbl_white_dice_throws_counter.Text = _whiteThrowsCounter.ToString();
                                    }));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            Mut.ReleaseMutex();
                        }
                    }
                    Debug.WriteLine(_rollResult.ToString());
                }
                if (_rollWhiteDiceThreadStopped)
                {
                    if (Mut.WaitOne())
                    {
                        try
                        {
                            if (!_transactionDisposed)
                            {
                                transaction.Commit();
                                transaction.Dispose();
                                _transactionDisposed = true;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            Mut.ReleaseMutex();
                        }
                    }
                }
                if (_rollWhiteDiceThreadAborted)
                {
                    if (Mut.WaitOne())
                    {
                        try
                        {
                            if (!_transactionDisposed)
                            {
                                transaction.Rollback();
                                transaction.Dispose();
                                _transactionDisposed = true;
                                ResetCounters(db);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            Mut.ReleaseMutex();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btn_stop_roll_Click(object sender, EventArgs e)
        {
            StopThread_RollBlackDice();
            StopThread_RollWhiteDice();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                DB db = new DB();
                //Check DB updates
                db.Initialize();

                RollResultService rollResultService = new RollResultService(db);

                _blackAcesCounter = rollResultService.GetNumberOfAces(DiceColor.Black);
                _whiteAcesCounter = rollResultService.GetNumberOfAces(DiceColor.White);
                lbl_black_dice_ace_counter.Text = _blackAcesCounter.ToString();
                lbl_white_dice_ace_counter.Text = _whiteAcesCounter.ToString();
                _blackThrowsCounter = rollResultService.GetNumberOfThrows(DiceColor.Black);
                _whiteThrowsCounter = rollResultService.GetNumberOfThrows(DiceColor.White);
                lbl_black_dice_throws_counter.Text = _blackThrowsCounter.ToString();
                lbl_white_dice_throws_counter.Text = _whiteThrowsCounter.ToString();
            }
            catch (Exception ex)
            {
                Log.Instance.LogMsg(ex.Message, Log.Level.Error);
            }
        }

        private void btn_cancel_roll_Click(object sender, EventArgs e)
        {
            AbortThread_RollBlackDice();
            AbortThread_RollWhiteDice();
        }

        private void btn_inject_rollResults_Click(object sender, EventArgs e)
        {

            DB db = new DB();

            if (db.Dices.Count() == 0)
            {
                MessageBox.Show("Please create dices first");
                return;
            }

            openFileDialog1.Filter = "JSON files (*.json)|*.json|XML files (*.xml)|*.xml";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName == string.Empty) return;
            if (openFileDialog1.FileName.ToLower().EndsWith("json"))
            {
                //Deserialize JSON file to RollResult
                FileStore fileStore = Factory.CreateFileStore("JSON");
                List<RollResult> rollResultList = fileStore.DeserializedRollResults(openFileDialog1.FileName);

                db.RollResults.AddRange(rollResultList);
                db.SaveChanges();

                //update labels
                UpdateLabelsAndCounters();

            }
            else if (openFileDialog1.FileName.ToLower().EndsWith("xml"))
            {
                //Deserialize XML file to RollResults
                FileStore fileStore = Factory.CreateFileStore("XML");
                List<RollResult> rollResultList = fileStore.DeserializedRollResults(openFileDialog1.FileName);

                db.RollResults.AddRange(rollResultList);
                db.SaveChanges();

                //update labels
                UpdateLabelsAndCounters();
            }
        }

        private void btn_store_rollResults_Click(object sender, EventArgs e)
        {            
            DB db = new DB();
            db.Configuration.ProxyCreationEnabled = false;

            if (db.Dices.Count() == 0)
            {
                MessageBox.Show("Please create dices first");
                return;
            }

            if (db.RollResults.Count() == 0)
            {
                MessageBox.Show("Nothing to store");
                return;
            }

            List<RollResult> rollResultList = db.RollResults.ToList();

            saveFileDialog1.Filter = "JSON files (*.json)|*.json|XML files (*.xml)|*.xml";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName == string.Empty) return;
            if (saveFileDialog1.FileName.ToLower().EndsWith("json"))
            {
                //Serialize RollResults to JSON file
                FileStore fileStore = Factory.CreateFileStore("JSON");
                string serializedList = fileStore.SerializedRollResults(rollResultList);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(saveFileDialog1.FileName);
                writer.Write(serializedList);
                writer.Close();
            }
            else if (saveFileDialog1.FileName.ToLower().EndsWith("xml"))
            {
                //Serialize RollResults to XML file
                FileStore fileStore = Factory.CreateFileStore("XML");
                string serializedList = fileStore.SerializedRollResults(rollResultList);
                //System.IO.File.WriteAllText(saveFileDialog1.FileName, serializedList, Encoding.);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(saveFileDialog1.FileName);
                writer.Write(serializedList);
                writer.Close();
            }
        }

        private void ResetCounters(DB db)
        {
            RollResultService rollResultService = new RollResultService(db);

            _blackAcesCounter = rollResultService.GetNumberOfAces(DiceColor.Black);
            if (!lbl_black_dice_ace_counter.IsDisposed)
            {
                lbl_black_dice_ace_counter.BeginInvoke((MethodInvoker)(() =>
                {
                    lbl_black_dice_ace_counter.Text = _blackAcesCounter.ToString();
                }));
            }

            _whiteAcesCounter = rollResultService.GetNumberOfAces(DiceColor.White);
            if (!lbl_white_dice_ace_counter.IsDisposed)
            {
                lbl_white_dice_ace_counter.BeginInvoke((MethodInvoker)(() =>
                {
                    lbl_white_dice_ace_counter.Text = _whiteAcesCounter.ToString();
                }));
            }

            _blackThrowsCounter = rollResultService.GetNumberOfThrows(DiceColor.Black);
            if (!lbl_black_dice_throws_counter.IsDisposed)
            {
                lbl_black_dice_throws_counter.BeginInvoke((MethodInvoker)(() =>
                {
                    lbl_black_dice_throws_counter.Text = _blackThrowsCounter.ToString();
                }));
            }

            _whiteThrowsCounter = rollResultService.GetNumberOfThrows(DiceColor.White);
            if (!lbl_white_dice_throws_counter.IsDisposed)
            {
                lbl_white_dice_throws_counter.BeginInvoke((MethodInvoker)(() =>
                {
                    lbl_white_dice_throws_counter.Text = _whiteThrowsCounter.ToString();
                }));
            }
        }
        private void UpdateLabelsAndCounters()
        {
            DB db = new DB();
            RollResultService rollResultService = new RollResultService(db);

            _blackAcesCounter = rollResultService.GetNumberOfAces(DiceColor.Black);
            lbl_black_dice_ace_counter.Text = _blackAcesCounter.ToString();
            _blackThrowsCounter = rollResultService.GetNumberOfThrows(DiceColor.Black);
            lbl_black_dice_throws_counter.Text = _blackThrowsCounter.ToString();
            _whiteAcesCounter = rollResultService.GetNumberOfAces(DiceColor.White);
            lbl_white_dice_ace_counter.Text = _whiteAcesCounter.ToString();
            _whiteThrowsCounter = rollResultService.GetNumberOfThrows(DiceColor.White);
            lbl_white_dice_throws_counter.Text = _whiteThrowsCounter.ToString();
        }
    }
}
