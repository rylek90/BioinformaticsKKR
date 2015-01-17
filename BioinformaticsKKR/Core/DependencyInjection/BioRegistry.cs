using BioinformaticsKKR.Core.Definitions.SimilarityMatrices;
using BioinformaticsKKR.Core.IO;
using BioinformaticsKKR.Service.Alignement;
using BioinformaticsKKR.Service.Assembly;
using BioinformaticsKKR.Service.Converter;
using BioinformaticsKKR.Service.Modification;
using BioinformaticsKKR.ViewModel;
using StructureMap.Configuration.DSL;

namespace BioinformaticsKKR.Core.DependencyInjection
{
    public class BioRegistry : Registry

    {
        public BioRegistry()
        {
            RegisterSimilarityMatrices();
            RegisterConverters();
            RegisterIoProviders();
            RegisterViewModels();
            RegisterAssemblers();
            RegisterSequenceAligners();
            RegisterModificatorSequences();
        }

        private void RegisterIoProviders()
        {
            For<ISequenceFileReader>().Use<SequenceFileReader>();
            For<ISequenceFileWriter>().Use<SequenceFileWriter>();
            For<IAmFileDialog>().Use<ReadFileDialog>();
            For<IAmFileDialog>().Use<WriteFileDialog>();
        }

        private void RegisterConverters()
        {
            For<ISequenceConverter>().Use<DnatoRnaSequenceConverter>();
            For<ISequenceConverter>().Use<RnaToDnaSequenceConverter>();
            For<ISequenceConverter>().Use<RnaToProteinSequenceConverter>();
            For<ISequenceConverter>().Use<DnaToProteinSequenceConverter>();
        }

        private void RegisterViewModels()
        {
            For<ISequencesLinksViewModel>().Use<SequencesLinksViewModel>();
            For<IAlignViewModel>().Use<AlignViewModel>();
            For<IMultiAlignViewModel>().Use<MultiAlignViewModel>();
            For<IManipulationViewModel>().Use<ManipulationViewModel>();

            For<ISequencesStatisticsLinksViewModel>().Use<SequencesStatisticsLinksViewModel>();
            For<ISequencesLinksViewModel>().Use<SequencesLinksViewModel>();
            For<IStatusViewModel>().Use<StatusViewModel>();

            For<IStatusViewModel>().Use<StatusViewModel>();

            For<IReadFileViewModel>()
                .Use<ReadFileViewModel>()
                .Ctor<ISequenceFileReader>()
                .Is<SequenceFileReader>()
                .Ctor<IAmFileDialog>()
                .Is<ReadFileDialog>();

            For<IWriteCollectionViewModel>()
                .Use<WriteCollectionViewModel>()
                .Ctor<ISequenceFileWriter>()
                .Is<SequenceFileWriter>()
                .Ctor<IAmFileDialog>()
                .Is<WriteFileDialog>();

            For<ICurrentSequenceViewModel>()
                .Use<CurrentSequenceViewModel>()
                .Ctor<ISequenceFileReader>()
                .Is<SequenceFileReader>();

            For<IManipulationSequenceViewModel>().Use<ManipulationSequenceViewModel>();
            For<IAlignmentSequenceViewModel>().Use<AlignmentSequenceViewModel>();
            For<ISingleSequenceViewModel>().Use<SingleSequenceViewModel>();
            For<IPamsamAlignerViewModel>().Use<PamsamAlignerViewModel>();
        }

        private void RegisterAssemblers()
        {
            For<IAssembleSequences>().Use<OverlapDeNovoAssemblerService>();
        }

        private void RegisterModificatorSequences()
        {
            For<IModificatorSequences>().Use<ComplementedModificatorSequences>();
            For<IModificatorSequences>().Use<ReverseComplementedModificatorSequences>();
            For<IModificatorSequences>().Use<ReverseModificatorSequences>();
        }

        private void RegisterSequenceAligners()
        {
            For<IAlignSequences>().Use<MuMmerSequenceAligner>();
            For<IAlignSequences>().Use<NeedlemanWunschSequenceAligner>();
            For<IAlignSequences>().Use<SmithWatermanSequenceAligner>();
            For<IAlignSequences>().Use<PairwiseOverlapSequenceAligner>();

            For<IPamsamMultipleSequenceAligner>().Use<PamsamMultipleSequenceAligner>();
        }

        private void RegisterSimilarityMatrices()
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
        }
    }
}