using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Bio.Algorithms.Alignment;
using Bio.Algorithms.Alignment.MultipleSequenceAlignment;
using BioinformaticsKKR.Core.Definitions.SimilarityMatrices;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Provider;
using BioinformaticsKKR.Service.Alignement;
using FirstFloor.ModernUI.Windows.Controls;

namespace BioinformaticsKKR.ViewModel
{
    public interface IPamsamAlignerViewModel
    {
        Array AllProfileScoreFunctionNames { get; }
        ProfileScoreFunctionNames CurrentProfileScoreFunctionName { get; set; }
        int GapExtendPenalty { get; set; }
        int NumberOfParitions { get; set; }
        int DegreeOfParallelism { get; set; }
        int KmerLength { get; set; }
        int GapPenalty { get; set; }
        ProfileAlignerNames CurrentProfileAlignerName { get; set; }
        Array AllProfileAlignerNames { get; }
        UpdateDistanceMethodsTypes CurrentUpdateDistanceMethodsType { get; set; }
        Array UpdateDistanceMethodsTypes { get; }
        DistanceFunctionTypes CurrentDistanceFunctionType { get; set; }
        Array DistanceFunctionTypes { get; }
        IAmSimilarityMatrix CurrentSimilarityMatrix { get; set; }
        CommandBase Align { get; set; }
        IAlignedSequence AlignedSequences { get; set; }
        IEnumerable<IAmSimilarityMatrix> SimilarityMatrices { get; set; }
        IPamsamMultipleSequenceAligner PamsamMultipleSequenceAligner { get; set; }
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class PamsamAlignerViewModel : ViewModelBase, IPamsamAlignerViewModel
    {
        private readonly IAlignmentSequenceViewModel _alignmentSequenceViewModel;
        private IAmSimilarityMatrix _currentSimilarityMatrix;
        private IAlignedSequence _alignedSequences;

        public Array AllProfileScoreFunctionNames
        {
            get { return Enum.GetValues(typeof (ProfileScoreFunctionNames)); }
        }

        public ProfileScoreFunctionNames CurrentProfileScoreFunctionName
        {
            get { return PamsamMultipleSequenceAligner.ProfileScoreFunctionNames; }
            set
            {
                PamsamMultipleSequenceAligner.ProfileScoreFunctionNames = value;
                OnPropertyChanged("CurrentProfileScoreFunctionName");
            }
        }

        public int GapExtendPenalty
        {
            get { return PamsamMultipleSequenceAligner.GapExtendPenalty; }
            set
            {
                PamsamMultipleSequenceAligner.GapExtendPenalty = value;
                OnPropertyChanged("GapExtendPenalty");
            }
        }

        public int NumberOfParitions
        {
            get { return PamsamMultipleSequenceAligner.NumberOfParitions; }
            set
            {
                PamsamMultipleSequenceAligner.NumberOfParitions = value;
                OnPropertyChanged("NumberOfParitions");
            }
        }


        public int DegreeOfParallelism
        {
            get { return PamsamMultipleSequenceAligner.DegreeOfParallelism; }
            set
            {
                PamsamMultipleSequenceAligner.DegreeOfParallelism = value;
                OnPropertyChanged("DegreeOfParallelism");
            }
        }

        public int KmerLength
        {
            get { return PamsamMultipleSequenceAligner.KmerLength; }
            set
            {
                PamsamMultipleSequenceAligner.KmerLength = value;
                OnPropertyChanged("KmerLength");
            }
        }

        public int GapPenalty
        {
            get { return PamsamMultipleSequenceAligner.GapPenalty; }
            set
            {
                PamsamMultipleSequenceAligner.GapPenalty = value;
                OnPropertyChanged("GapPenalty");
            }
        }

        public ProfileAlignerNames CurrentProfileAlignerName
        {
            get { return PamsamMultipleSequenceAligner.ProfileAlignerNames; }
            set
            {
                PamsamMultipleSequenceAligner.ProfileAlignerNames = value;
                OnPropertyChanged("ProfileAlignerNames");
            }
        }

        public Array AllProfileAlignerNames
        {
            get { return Enum.GetValues(typeof (ProfileAlignerNames)); }
        }


        public UpdateDistanceMethodsTypes CurrentUpdateDistanceMethodsType
        {
            get { return PamsamMultipleSequenceAligner.UpdateDistanceMethodsTypes; }
            set
            {
                PamsamMultipleSequenceAligner.UpdateDistanceMethodsTypes = value;
                OnPropertyChanged("CurrentUpdateDistanceMethodsType");
            }
        }

        public Array UpdateDistanceMethodsTypes
        {
            get { return Enum.GetValues(typeof (UpdateDistanceMethodsTypes)); }
        }


        public DistanceFunctionTypes CurrentDistanceFunctionType
        {
            get { return PamsamMultipleSequenceAligner.DistanceFunctionTypes; }
            set
            {
                PamsamMultipleSequenceAligner.DistanceFunctionTypes = value;
                OnPropertyChanged("CurrentDistanceFunctionType");
            }
        }

        public Array DistanceFunctionTypes
        {
            get { return Enum.GetValues(typeof (DistanceFunctionTypes)); }
        }


        public IAmSimilarityMatrix CurrentSimilarityMatrix
        {
            get { return PamsamMultipleSequenceAligner.SimilarityMatrix; }
            set
            {
                PamsamMultipleSequenceAligner.SimilarityMatrix = value;
                OnPropertyChanged("CurrentSimilarityMatrix");
            }
        }


        public PamsamAlignerViewModel(IEnumerable<IAmSimilarityMatrix> similarityMatrices,
            IAlignmentSequenceViewModel alignmentSequenceViewModel,
            IPamsamMultipleSequenceAligner pamsamMultipleSequenceAligner)
        {
            _alignmentSequenceViewModel = alignmentSequenceViewModel;
            PamsamMultipleSequenceAligner = pamsamMultipleSequenceAligner;
            SimilarityMatrices = similarityMatrices;

            _alignmentSequenceViewModel.PropertyChanged += (sender, e) => OnPropertyChanged(e.ToString());
            Align = new CommandBase
            {
                CanExecuteMethod = CanExecuteAlign,
                ExecuteMethod = ExecuteAlign
            };
        }

        public IAlignmentSequenceViewModel AlignmentSequenceViewModel
        {
            get { return _alignmentSequenceViewModel; }
        }

        private bool CanExecuteAlign(object obj)
        {
            return CurrentSimilarityMatrix != null;
        }

        public CommandBase Align { get; set; }

        private async void ExecuteAlign(object obj)
        {
            try
            {
                await Task.Run(() =>
                {
                    var alp = SequencesRepository.Instance.Sequences.First().Alphabet;
                    var result = PamsamMultipleSequenceAligner.Align(alp, SequencesRepository.Instance.Sequences.ToArray());


                    var sequencess = result.First().AlignedSequences.First();
                    AlignedSequences = sequencess;
                });
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "Warning!", MessageBoxButton.OK);
            }
        }

        public IAlignedSequence AlignedSequences
        {
            get { return _alignedSequences; }
            set
            {
                _alignedSequences = value; 
                OnPropertyChanged("AlignedSequences"); 
            }
        }

        public IEnumerable<IAmSimilarityMatrix> SimilarityMatrices { get; set; }

        public IPamsamMultipleSequenceAligner PamsamMultipleSequenceAligner { get; set; }

        new void OnPropertyChanged(string propertyName)
        {
            Align.UpdateCanExecuteState();
            base.OnPropertyChanged(propertyName);
        }
    }
}