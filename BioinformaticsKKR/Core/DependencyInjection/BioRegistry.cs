﻿using BioinformaticsKKR.Core.IO;
using BioinformaticsKKR.Service.Converter;
using BioinformaticsKKR.ViewModel;
using StructureMap.Configuration.DSL;

namespace BioinformaticsKKR.Core.DependencyInjection
{
    public class BioRegistry : Registry
    
    {
        public BioRegistry()
        {
            For<ISequenceFileReader>().Use<SequenceFileReader>();
            For<ISequenceFileWriter>().Use<SequenceFileWriter>();

            For<ISequenceConverter>().Use<DnatoRnaSequenceConverter>();
            For<ISequenceConverter>().Use<RnaToDnaSequenceConverter>();
            For<ISequenceConverter>().Use<RnaToProteinConverter>();

            For<IAmFileDialog>().Use<ReadFileDialog>();
            For<IAmFileDialog>().Use<WriteFileDialog>();

            For<ISequencesLinksViewModel>().Use<SequencesLinksViewModel>();

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
        }
    }
}