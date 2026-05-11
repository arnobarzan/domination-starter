using Xunit;

namespace DominationGame.Tests
{
    // Prevents parallel execution across test classes that share the domination.txt file.
    [CollectionDefinition("Sequential", DisableParallelization = true)]
    public class SequentialCollection { }
}
