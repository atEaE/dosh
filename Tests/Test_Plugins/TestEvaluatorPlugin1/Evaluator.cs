using Dosh.Core.Provider.Evaluator;
using System;

namespace TestEvaluatorPlugin1
{
    /// <summary>
    /// TestEvaluatorPlugin1
    /// </summary>
    public class Evaluator : IEvaluator
    {
        /// <summary>
        /// Specific evaluation of implementation results.
        /// </summary>
        public void Evaluate()
        {
            Console.WriteLine("TestEvaluatorPlugin1 Called.");
        }
    }
}
