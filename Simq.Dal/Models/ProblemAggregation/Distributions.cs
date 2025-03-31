namespace Simq.Dal.Models;

public interface IDistribution;

public record TDistribution : IDistribution
{
    public double A { get; init; }
}

public record RayleighDistribution : IDistribution
{
    public double Sigma { get; init; }
}

public record PoissonDistribution : IDistribution
{
    public double ExpRateInv { get; init; }
}

public record PascalDistribution : IDistribution
{
    public double P { get; init; }
    public int R { get; init; }
}

public record NormalDistribution : IDistribution
{
    public double Mu { get; init; }
    public double Sigma { get; init; }
}

public record HypergeometricDistribution : IDistribution
{
    public int N { get; init; }
    public int n { get; init; }
    public int K { get; init; }
}

public record GeometricDistibution : IDistribution
{
    public double P { get; init; }
}

public record GammaDistribution : IDistribution
{
    public double K { get; init; }
    public double Theta { get; init; }
}

public record FDistribution : IDistribution
{
    public double A { get; init; }
    public double B { get; init; }
}

public record ExponentialDistribution : IDistribution
{
    public double Rate { get; init; }
}

public record BinomialDistribution : IDistribution
{
    public double p { get; init; }
    public int n { get; init; }
}

public record BetaDistribution : IDistribution
{
    public double Alpha { get; init; }
    public double Beta { get; init; }
}

public record BernoulliDistribution : IDistribution
{
    public double P { get; init; }
}