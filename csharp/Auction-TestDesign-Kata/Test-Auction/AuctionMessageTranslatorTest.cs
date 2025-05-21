using Auction;

namespace Test_Auction_TestDesign_Kata;

public class AuctionMessageTranslatorTest
{
    [Fact]
    public void NotifiesAuctionClosedWhenCloseMessageReceived()
    {
        var message = "SOLVersion: 1.1; Event: CLOSE;";
        var mockAuctionEventListener = new MockAuctionEventListener();
        mockAuctionEventListener.ExpectAuctionClosed();
        var target = new AuctionMessageTranslator(mockAuctionEventListener);

        target.ProcessMessage(message);

        mockAuctionEventListener.AssertExpectations();
    }

    [Fact]
    public void NotifiesBidDetailsWhenPriceMessageReceived()
    {
        var message = "SOLVersion: 1.1; Event: PRICE; CurrentPrice: 192; Increment: 7; Bidder: Someone else;";
        // TODO: write a test for this message translation
        var mockAuctionEventListener = new MockAuctionEventListener();
        mockAuctionEventListener.ExpectPriceChanged(currentPrice: 192, incrememt: 7, bidder: "Someone else;");
        var target = new AuctionMessageTranslator(mockAuctionEventListener);

        target.ProcessMessage(message);

        mockAuctionEventListener.AssertExpectations();
    }
}

public class MockAuctionEventListener : IAuctionEventListener
{
    private bool expectAuctionClosed = false;
    private bool actualAuctionClosed = false;
    private bool expectPriceChanged;
    private bool actualPriceChanged;
    private int expectCurrentPrice;
    private int actualCurrentPrice;
    //
    // private int expectIncrememt;
    // private string expectBidder;

    public void ExpectAuctionClosed()
    {
        expectAuctionClosed = true;
    }

    public void AssertExpectations()
    {
        Assert.Equal(expectAuctionClosed, actualAuctionClosed);
        Assert.Equal(expectPriceChanged, actualPriceChanged);
        Assert.Equal(expectCurrentPrice, actualCurrentPrice);
    }

    public void AuctionClosed()
    {
        actualAuctionClosed = true;
    }

    public void PriceChanged()
    {
        actualPriceChanged = true;
    }

    public void ExpectPriceChanged(int currentPrice, int incrememt, string bidder)
    {
        expectPriceChanged = true;
        expectCurrentPrice = currentPrice;
        // expectIncrement = incrememt;
        // expectBidder = bidder;
    }
}