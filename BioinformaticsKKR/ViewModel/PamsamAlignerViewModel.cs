using System;
using System.Collections.Generic;
using System.ComponentModel;
using Bio.Algorithms.Alignment.MultipleSequenceAlignment;
using Bio.SimilarityMatrices;
using BioinformaticsKKR.Core.Definitions.SimilarityMatrices;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Service.Alignement;

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
        IEnumerable<IAmSimilarityMatrix> SimilarityMatrices { get; }
        IPamsamMultipleSequenceAligner PamsamMultipleSequenceAligner { get; set; }
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class PamsamAlignerViewModel : ViewModelBase, IPamsamAlignerViewModel
    {
        private IAmSimilarityMatrix _currentSimilarityMatrix;

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
            get { return _currentSimilarityMatrix; }
            set
            {
                _currentSimilarityMatrix = value;
                OnPropertyChanged("CurrentSimilarityMatrix");
            }
        }


        public PamsamAlignerViewModel(IEnumerable<IAmSimilarityMatrix> similarityMatrices,
            IPamsamMultipleSequenceAligner pamsamMultipleSequenceAligner)
        {
            PamsamMultipleSequenceAligner = pamsamMultipleSequenceAligner;
            SimilarityMatrices = similarityMatrices;
            Align = new CommandBase
            {
                CanExecuteMethod = o => true,
                ExecuteMethod = ExecuteAlign
            };
        }

        public CommandBase Align { get; set; }

        private void ExecuteAlign(object obj)
        {
            PamsamMultipleSequenceAligner.Align(null, null);
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