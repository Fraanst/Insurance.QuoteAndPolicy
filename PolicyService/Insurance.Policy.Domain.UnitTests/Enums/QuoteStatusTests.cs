
public class QuoteStatusTests
{
    [Fact]
    public void QuoteStatus_ShouldHaveExpectedMembers()
    {
        // ARRANGE & ACT
        var memberNames = Enum.GetNames(typeof(QuoteStatus));

        // ASSERT
        Assert.Equal(3, memberNames.Length);

        Assert.Contains("UnderReview", memberNames);
        Assert.Contains("Approved", memberNames);
        Assert.Contains("Rejected", memberNames);
    }

    [Fact]
    public void QuoteStatus_ShouldHaveCorrectUnderlyingIntegerValues()
    {
        // ARRANGE & ACT
        Assert.Equal(0, (int)QuoteStatus.UnderReview);
        Assert.Equal(1, (int)QuoteStatus.Approved);
        Assert.Equal(2, (int)QuoteStatus.Rejected);
    }

    [Fact]
    public void QuoteStatus_ShouldParseFromStringCorrectly()
    {
        // ARRANGE
        var stringValue = "Approved";

        // ACT
        var parsedValue = Enum.Parse<QuoteStatus>(stringValue);

        // ASSERT
        Assert.Equal(QuoteStatus.Approved, parsedValue);
    }
}