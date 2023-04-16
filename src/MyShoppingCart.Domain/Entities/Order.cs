using MyShoppingCart.Domain.Models;
using System.Text.Json.Serialization;

namespace MyShoppingCart.Domain.Entities;

public sealed class Order : IEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]  
    public Customer? Customer { get; set; }
    public DateTime OrderDateTimeUtc { get; set; }
    public IReadOnlyCollection<LineItem> LineItems => _lineItems;
    private HashSet<LineItem> _lineItems = new();

    public void AddUpdate(LineItem lineItem)
    {
        lineItem.OrderId = Id;

        var foundLineItem = _lineItems.FirstOrDefault(x => x.Id == lineItem.Id);
        if (foundLineItem is not null)
        {
            if (foundLineItem != lineItem)
            {
                lineItem.Adapt(foundLineItem);
            }
            return;
        }

        _lineItems.Add(lineItem);
    }

    public void AddUpdate(LineItemModel lineItemModel)
    {
        var lineItem = new LineItem(Id, lineItemModel.ProductId, lineItemModel.Quantity);
        _lineItems.Add(lineItem);
    }

    public void AddUpdateRange(IEnumerable<LineItem> lineItems)
    {
        foreach (var lineItem in lineItems)
        {
            AddUpdate(lineItem);
        }
    }

    public void AddUpdateRange(IEnumerable<LineItemModel> lineItemModels)
    {
        foreach (var lineItemModel in lineItemModels)
        {
            AddUpdate(lineItemModel);
        }
    }

    public void Remove(LineItem lineItem)
    {
        Remove(lineItem.Id);
    }

    public void Remove(Guid lineItemId)
    {
        var foundLineItem = _lineItems.FirstOrDefault(x => x.Id == lineItemId);
        if (foundLineItem is not null)
        {
            _lineItems.Remove(foundLineItem);
        }
    }

    #region Equatable
    public bool Equals(Order? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (ReferenceEquals(null, other))
            return false;

        return
            Id == other.Id &&
            CustomerId == other.CustomerId &&
            OrderDateTimeUtc == other.OrderDateTimeUtc &&
            LineItems.SequenceEqual(other.LineItems);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Order);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, CustomerId, OrderDateTimeUtc, LineItems);
    }

    public static bool operator ==(Order obj1, Order obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true;
        if (ReferenceEquals(obj1, null))
            return false;
        if (ReferenceEquals(obj2, null))
            return false;
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Order obj1, Order obj2) => !(obj1 == obj2);

    #endregion
}
