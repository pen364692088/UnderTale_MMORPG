using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    [Serializable]
    public class MatchRoomDto {

        public Dictionary<int, UserDto> uidUserDict;

        public List<int> readyUidList;

        public List<int> uidList; //进入顺序



        public MatchRoomDto() {
            this.uidUserDict = new Dictionary<int,UserDto>();
            this.readyUidList = new List<int>();
            this.uidList = new List<int>();
        }
        public MatchRoomDto(Dictionary<int, UserDto> uidUserDict, List<int> readyUidList) {
            this.uidUserDict = uidUserDict;
            this.readyUidList = readyUidList;
        }
        public void Add(UserDto user) {
            this.uidUserDict.Add(user.Id, user);
            this.uidList.Add(user.Id);
        }

        public void Leave(UserDto user) {
            this.uidUserDict.Remove(user.Id);
            this.uidList.Remove(user.Id);
        }
        public void Ready(int uid) {
            this.readyUidList.Add(uid);
        }

        public bool isReadyAll() {
            return this.readyUidList.Count == 3;
        }

        public int LUid = -1;
        public int RUid = -1;

        public void ReSetPosition(int myId) {
            LUid = -1;
            RUid = -1;
            switch (uidList.Count) {
                case 1: {

                    break;
                    }
                case 2: {
                    if (uidList[0] == myId) {
                        RUid = uidList[1];
                    }
                    else if (uidList[1] == myId) {
                        LUid = uidList[0];
                    }
                        break;
                    }
                case 3: {
                    if (uidList[0] == myId) {
                        LUid = uidList[2];
                        RUid = uidList[1];
                    }
                    else if (uidList[1] == myId) {
                        LUid = uidList[0];
                        RUid = uidList[2];
                    }
                    else if (uidList[2] == myId) {
                        LUid = uidList[1];
                        RUid = uidList[0];
                    }
                        break;
                    }
            }
        }

    

}
