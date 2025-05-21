namespace Auction;

public interface IAuctionEventListener
{
    void AuctionClosed();
    void PriceChanged();
}