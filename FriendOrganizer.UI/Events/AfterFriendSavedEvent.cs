using Prism.Events;

namespace FriendOrganizer.UI.Events
{
    public class AfterFriendSavedEvent: PubSubEvent<AfterFriendSaveEventArgs>
    {
    }

    public class AfterFriendSaveEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }
}
