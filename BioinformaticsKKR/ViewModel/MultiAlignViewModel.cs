using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Bio;
using Bio.SimilarityMatrices;
using BioinformaticsKKR.Core.Definitions.SimilarityMatrices;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Service.Alignement;
using FirstFloor.ModernUI.Windows.Controls;

namespace BioinformaticsKKR.ViewModel
{
    public interface IMultiAlignViewModel
    {
        string Status { get; set; }
        CommandBase AlignCommand { get; set; }
        int GapPenalty { get; set; }
        IEnumerable<IAmSimilarityMatrix> SimilarityMatrices { get; set; }
        IAmSimilarityMatrix CurrentSimilarityMatrix { get; set; }
        List<IAlignSequences> SequencesAligners { get; set; }
        IAlignSequences CurrentAligner { get; set; }
        ISingleSequenceViewModel SingleSequenceViewModel { get; set; }
        ObservableCollection<ISequence> AvailableSequencesList { get; set; }
        ISequence FirstSequenceSelected { get; set; }
        ObservableCollection<ISequence> SelectedSequencesList { get; set; }
        ISequence SecondSequenceSelected { get; set; }
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class MultiAlignViewModel : ViewModelBase, IMultiAlignViewModel
    {
        private readonly IEnumerable<IAlignSequences> _sequencesAligners;
        private IAlignSequences _currentAligner;
        private IAmSimilarityMatrix _currentSimilarityMatrix;
        private readonly IStatusViewModel _statusService;
        private ObservableCollection<ISequence> _selectedSequencesList;
        private ObservableCollection<ISequence> _availableSequencesList;
        private ISequence _secondSequenceSelected;
        private ISequence _firstSequenceSelected;
        private int _gapPenalty;
        private ISequence _chosen;
        private List<IAlignSequences> _availableSequenceAligners;
        private IEnumerable<IAmSimilarityMatrix> _availableSimilarityMatrices;
        private readonly IEnumerable<IAmSimilarityMatrix> _similarityMatrices;
        private ISingleSequenceViewModel _singleSequenceViewModel;

        public string Status
        {
            get { return _statusService.LastStatus; }
            set
            {
                _statusService.LastStatus = value;
                OnPropertyChanged("Status");
            }
        }

        public MultiAlignViewModel(IEnumerable<IAlignSequences> sequencesAligners,
            IStatusViewModel statusService,
            IEnumerable<IAmSimilarityMatrix> similarityMatrices,
            ISingleSequenceViewModel singleSequenceViewModel
            )
        {
            _statusService = statusService;
            _statusService.PropertyChanged += (sender, e) => OnPropertyChanged(e.ToString());
            _sequencesAligners = sequencesAligners;
            _similarityMatrices = similarityMatrices;
            SingleSequenceViewModel = singleSequenceViewModel;
            SingleSequenceViewModel.PropertyChanged += (sender, e) => OnPropertyChanged(e.ToString());

            AddToSelected = new CommandBase
            {
                CanExecuteMethod = CanAddToSelected,
                ExecuteMethod = ExecuteAddToSelected,
            };

            AlignCommand = new CommandBase
            {
                CanExecuteMethod = CanAlign,
                ExecuteMethod = ExecuteAlign
            };

            RemoveFromSelected = new CommandBase
            {
                CanExecuteMethod = CanRemoveFromSelected,
                ExecuteMethod = ExecuteRemoveFromSelected,
            };
        }

        public CommandBase RemoveFromSelected { get; set; }

        private void ExecuteRemoveFromSelected(object obj)
        {
            var sequenceToRemove = SecondSequenceSelected;
            SelectedSequencesList.Remove(SecondSequenceSelected);
            SecondSequenceSelected = null;
            FirstSequenceSelected = sequenceToRemove;
            AvailableSequencesList.Add(sequenceToRemove);
        }

        private bool CanRemoveFromSelected(object obj)
        {
            return _secondSequenceSelected != null;
        }

        public CommandBase AddToSelected { get; set; }

        private void ExecuteAddToSelected(object obj)
        {
            var sequenceToAdd = FirstSequenceSelected;
            AvailableSequencesList.Remove(FirstSequenceSelected);
            FirstSequenceSelected = null;
            SelectedSequencesList.Add(sequenceToAdd);
        }

        private bool CanAddToSelected(object obj)
        {
            return AvailableSequencesList != null
                   && AvailableSequencesList.Any();
        }

        public CommandBase AlignCommand { get; set; }

        private async void ExecuteAlign(object obj)
        {
            try
            {
                await Task.Run(() =>
                {
                    _currentAligner.GapPenalty = GapPenalty;
                    _currentAligner.SimilarityMatrix = new SimilarityMatrix(_currentSimilarityMatrix.Matrix);
                    var sequence = _currentAligner.Align(FirstSequenceSelected, SecondSequenceSelected);

                    // WTF!!!!!!
                    //Chosen = sequence.First().PairwiseAlignedSequences.First().Consensus;
                });
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "Warning!", MessageBoxButton.OK);
            }
        }

        public int GapPenalty
        {
            get { return _gapPenalty; }
            set
            {
                _gapPenalty = value;
                OnPropertyChanged("GapPenalty");
            }
        }

        private bool CanAlign(object obj)
        {
            return SelectedSequencesList != null 
                && SelectedSequencesList.Any();
        }

        public IEnumerable<IAmSimilarityMatrix> SimilarityMatrices
        {
            get { return _availableSimilarityMatrices; }
            set
            {
                _availableSimilarityMatrices = value;
                OnPropertyChanged("SimilarityMatrices");
            }
        }

        public IAmSimilarityMatrix CurrentSimilarityMatrix
        {
            get { return _currentSimilarityMatrix; }
            set
            {
                _currentSimilarityMatrix = value;
                AlignCommand.UpdateCanExecuteState();
                OnPropertyChanged("CurrentSimilarityMatrix");
            }
        }


        public List<IAlignSequences> SequencesAligners
        {
            get { return _availableSequenceAligners; }
            set
            {
                _availableSequenceAligners = value;
                OnPropertyChanged("SequencesAligners");
            }
        }

        public IAlignSequences CurrentAligner
        {
            get { return _currentAligner; }
            set
            {
                _currentAligner = value;
                AlignCommand.UpdateCanExecuteState();
                OnPropertyChanged("CurrentAligner");
            }
        }

        public ISingleSequenceViewModel SingleSequenceViewModel
        {
            get { return _singleSequenceViewModel; }
            set
            {
                if (value != null)
                {
                    _singleSequenceViewModel = value;
                    OnPropertyChanged("SingleSequenceViewModel");
                }
            }
        }

        public ObservableCollection<ISequence> AvailableSequencesList
        {
            get { return _availableSequencesList; }
            set
            {
                _availableSequencesList = value;
                OnPropertyChanged("AvailableSequencesList");
            }
        }

        public ISequence FirstSequenceSelected
        {
            get { return _firstSequenceSelected; }
            set
            {
                _firstSequenceSelected = value;
                AddToSelected.UpdateCanExecuteState();
                OnPropertyChanged("FirstSequenceSelected");
            }
        }

        public ObservableCollection<ISequence> SelectedSequencesList
        {
            get { return _selectedSequencesList; }
            set
            {
                _selectedSequencesList = value;
                AlignCommand.UpdateCanExecuteState();
                OnPropertyChanged("SecondSequenceList");
            }
        }

        public ISequence SecondSequenceSelected
        {
            get { return _secondSequenceSelected; }
            set
            {
                _secondSequenceSelected = value;
                AlignCommand.UpdateCanExecuteState();
                RemoveFromSelected.UpdateCanExecuteState();
                OnPropertyChanged("SecondSequenceSelected");
            }
        }
    }
}