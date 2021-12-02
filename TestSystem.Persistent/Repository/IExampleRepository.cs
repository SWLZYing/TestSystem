
namespace TestSystem.Persistent.Repository
{
    using System;
    using System.Collections.Generic;
    using TestSystem.Persistent.Model;

    public interface IExampleRepository
    {
        (Exception exception, IEnumerable<Example> examples) Query();
    }
}
