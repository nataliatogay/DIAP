using brownshouse.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.ViewModels
{
    public class AddNewDisciplineViewModel : IAddNewDisciplineViewModel
    {
        private IBusinessLogic _businessLogic;
        private IAddNewDisciplineView _view;
        public string DisciplineCode { get; set; }
        public string NewDiscipline { get; set; }

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
                                await _businessLogic.NewDisciplineAsync(NewDiscipline, DisciplineCode);
                                _view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (!String.IsNullOrWhiteSpace(NewDiscipline) && !String.IsNullOrEmpty(NewDiscipline) &&
                                    !String.IsNullOrWhiteSpace(DisciplineCode) && !String.IsNullOrEmpty(DisciplineCode));
                        }
                    );
                }
                return this.okCommand;
            }
        }


        public AddNewDisciplineViewModel(IAddNewDisciplineView view, IBusinessLogic logic)
        {
            this._businessLogic = logic;
            this._view = view;
            this._view.SetViewModel(this);
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
