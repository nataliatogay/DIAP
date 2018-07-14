using brownshouse.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.ViewModels
{
    public class AddNewFormViewModel : NotifyPropertyChangedObject, IAddNewFormViewModel
    {
        private IBusinessLogic _businessLogic;
        private IAddNewFormView _view;
        public string FormTitle { get; set; }
        private string filePath;
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
                OnPropertyChanged();
            }
        }

        private ICommand okCommand;
        public ICommand OkCommand
        {
            get
            {
                if (this.okCommand is null)
                {
                    this.okCommand = new RelayCommand(
                        async (param) =>
                        {
                            try
                            {
                                await _businessLogic.AddNewFormAsync(FormTitle, FilePath);
                                _view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (!String.IsNullOrWhiteSpace(FormTitle) && !String.IsNullOrEmpty(FormTitle) &&
                                    !String.IsNullOrWhiteSpace(FilePath) && !String.IsNullOrEmpty(FilePath));
                        }
                    );
                }
                return this.okCommand;
            }
        }
        
        private ICommand openCommand;
        public ICommand OpenCommand
        {
            get
            {
                if (this.openCommand is null)
                {
                    this.openCommand = new RelayCommand(
                        (param) =>
                        {
                            try
                            {
                                OpenFileDialog openFileDialog = new OpenFileDialog(); // { Filter = "Text Documents(*.txt)|*.txt" };
                                var res = openFileDialog.ShowDialog();
                                if (res.HasValue && res.Value)
                                {
                                    FilePath = openFileDialog.FileName;
                                }
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        }
                    );
                }
                return this.openCommand;
            }
        }

        public AddNewFormViewModel(IAddNewFormView view, IBusinessLogic logic)
        {
            this._view = view;
            this._businessLogic = logic;
            this._view.SetViewModel(this);
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
