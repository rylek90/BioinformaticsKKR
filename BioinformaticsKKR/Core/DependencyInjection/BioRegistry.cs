using BioinformaticsKKR.Core.Definitions.SimilarityMatrices;
using BioinformaticsKKR.Core.IO;
using BioinformaticsKKR.Service.Alignement;
using BioinformaticsKKR.Service.Assembly;
using BioinformaticsKKR.Service.Converter;
using BioinformaticsKKR.ViewModel;
using StructureMap.Configuration.DSL;

namespace BioinformaticsKKR.Core.DependencyInjection
{
    public class BioRegistry : Registry
    
    {
        public BioRegistry()
        {
            For<IAmSimilarityMatrix>().Use<AmbiguousDna>();
            For<IAmSimilarityMatrix>().Use<AmbiguousRna>();
            For<IAmSimilarityMatrix>().Use<Blosum45>();
            For<IAmSimilarityMatrix>().Use<Blosum50>();
            For<IAmSimilarityMatrix>().Use<Blosum62>();
            For<IAmSimilarityMatrix>().Use<Blosum80>();
            For<IAmSimilarityMatrix>().Use<Blosum90>();
            For<IAmSimilarityMatrix>().Use<DiagonalScoreMatrix>();
            For<IAmSimilarityMatrix>().Use<EDnaFull>();
            For<IAmSimilarityMatrix>().Use<Pam250>();
            For<IAmSimilarityMatrix>().Use<Pam30>();
            For<IAmSimilarityMatrix>().Use<Pam70>();

            For<ISequenceFileReader>().Use<SequenceFileReader>();
            For<ISequenceFileWriter>().Use<SequenceFileWriter>();

            For<ISequenceConverter>().Use<DnatoRnaSequenceConverter>();
            For<ISequenceConverter>().Use<RnaToDnaSequenceConverter>();
            For<ISequenceConverter>().Use<RnaToProteinSequenceConverter>();
            For<ISequenceConverter>().Use<DnaToProteinSequenceConverter>();

            For<IAlignViewModel>().Use<AlignViewModel>();
            For<ISequencesStatisticsLinksViewModel>().Use<SequencesStatisticsLinksViewModel>();
            For<ISequencesLinksViewModel>().Use<SequencesLinksViewModel>();
            For<IStatusViewModel>().Use<StatusViewModel>();


            For<IAmFileDialog>().Use<ReadFileDialog>();
            For<IAmFileDialog>().Use<WriteFileDialog>();

            For<ISequencesLinksViewModel>().Use<SequencesLinksViewModel>();

            For<IManipulationViewModel>().Use<ManipulationViewModel>();
            For<IAlignViewModel>().Use<AlignViewModel>();
            For<IStatusViewModel>().Use<StatusViewModel>();

            For<IReadFileViewModel>()
                .Use<ReadFileViewModel>()
                .Ctor<ISequenceFileReader>()
                .Is<SequenceFileReader>()
                .Ctor<IAmFileDialog>()
                .Is<ReadFileDialog>();
                

            For<ICurrentSequenceViewModel>()
                .Use<CurrentSequenceViewModel>()
                .Ctor<ISequenceFileReader>()
                .Is<SequenceFileReader>();

            For<IAlignmentSequenceViewModel>().Use<AlignmentSequenceViewModel>();

            For<IAssembleSequences>().Use<OverlapDeNovoAssemblerService>();

            For<IAlignSequences>().Use<MuMmerSequenceAligner>();
            For<IAlignSequences>().Use<NeedlemanWunschSequenceAligner>();
            For<IAlignSequences>().Use<SmithWatermanSequenceAligner>();
            For<IAlignSequences>().Use<PairwiseOverlapSequenceAligner>();
        }
    }
}