namespace WebApp.Template.DP.UserCards;

public class DefaultUserCardTemplate : UserCardTemplate
{
    protected override string SetFooter() => string.Empty;

    protected override string SetPicture() => $"<img class='card-img-top' src='/userpictures/defaultuserpicture.png'>";
}