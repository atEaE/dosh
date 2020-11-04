using Dosh.Core.Provider.Evaluator;
using System;

namespace TestEvaluatorPlugin2
{
    /// <summary>
    /// TestEvaluatorPlugin2
    /// </summary>
    public class Evaluator : IEvaluator
    {
        /// <summary>
        /// Specific evaluation of implementation results.
        /// </summary>
        public void Evaluate()
        {
            Console.WriteLine("TestEvaluatorPlugin2 Called.");
        }
    }
}
