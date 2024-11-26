namespace CommunityToolkit.Maui.Markup.Services;

static class GestureExtensions
{
    public static TGestureElement ConfigureSwipeGesture<TGestureElement>(this TGestureElement gestureElement,
        SwipeGestureRecognizer swipeGesture,
        SwipeDirection? direction = null,
        uint? threshold = null) where TGestureElement : IGestureRecognizers
    {
        if (direction is not null)
        {
            swipeGesture.Direction = direction.Value;
        }

        if (threshold is not null)
        {
            swipeGesture.Threshold = threshold.Value;
        }

        return gestureElement;
    }

    public static TGestureElement ConfigureTapGesture<TGestureElement>(this TGestureElement gestureElement,
        TapGestureRecognizer tapGesture,
        int? numberOfTapsRequired = null) where TGestureElement : IGestureRecognizers
    {
        if (numberOfTapsRequired is not null)
        {
            tapGesture.NumberOfTapsRequired = numberOfTapsRequired.Value;
        }

        return gestureElement;
    }
}