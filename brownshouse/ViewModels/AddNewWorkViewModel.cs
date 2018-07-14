using brownshouse.Domain.Models;
using brownshouse.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.ViewModels
{
    public class AddNewWorkViewModel : IAddNewWorkViewModel
    {
        private IBusinessLogic _businessLogic;
        private IAddNewWorkView _view;
        public string NewWork { get; set; }
        public IEnumerable<Discipline> DisciplinesList { get; set; }
        public Discipline SelectedDiscipline { get; set; }

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
                                await _businessLogic.AddNewWorkAsync(NewWork, SelectedDiscipline);
                                _view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (!String.IsNullOrWhiteSpace(NewWork) && !String.IsNullOrEmpty(NewWork) &&
                                    SelectedDiscipline != null);
                        }
                    );
                }
                return this.okCommand;
            }
        }

        public AddNewWorkViewModel(IAddNewWorkView view, IBusinessLogic logic)
        {
            this._businessLogic = logic;
            this._view = view;
            this._view.SetViewModel(this);
            try
            {
                DisciplinesList = _businessLogic.GetAllDisciplines().OrderBy(d => d.Title).ToList();
            }
            catch (Exception ex)
            {
                _view.Alert(ex.Message, "Error");
            }
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
