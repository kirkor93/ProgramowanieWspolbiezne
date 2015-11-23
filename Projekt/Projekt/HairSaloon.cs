using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class HairSaloon
    {
        private List<Human> _waitingRoom;
        private Human _hairDressingChair;

        public Object EnterLock;
        public Object ExitLock;
        public Object ChairLock;

        public int Capacity = 5;

        public HairSaloon()
        {
            _waitingRoom = new List<Human>();
            _hairDressingChair = null;

            EnterLock = new object();
            ExitLock = new object();
            ChairLock = new object();
        }

        public void EnterWaitRoom(Human human)
        {
            _waitingRoom.Add(human);
        }

        public void EnterChairRoom(Human human)
        {
            _hairDressingChair = human;
            _waitingRoom.Remove(human);
        }

        public void ExitChairRoom(Human human)
        {
            _hairDressingChair = null;
            _waitingRoom.Add(human);
        }

        public bool CanEnterChairRoom()
        {
            if (_hairDressingChair != null)
                return true;
            else
                return false;
        }

        public void ExitWaitRoom(Human human)
        {
            _waitingRoom.Remove(human);
        }
    }
}
