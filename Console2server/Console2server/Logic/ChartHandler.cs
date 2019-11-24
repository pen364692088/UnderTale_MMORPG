
using protocol.codes;
using server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class ChartHandler:IHandler {
        UserCache users = Caches.user;
        MatchCache matchroom = Caches.match;
        public void OnRecive(clientPeer client, int subCode, object value) {
            switch (subCode) {
                case ChartCode.CREQ: {
                    ChartDto chart = value as ChartDto;
                    chart_BRO(client,chart);
                    break;
                 }
            }
        }

        public void chart_BRO(clientPeer client, ChartDto chart) {
            if (!isLegal(client, 2)) {
                return;
            }
            int uid =users.getId(client);
            MatchRoom room= matchroom.getRoom(uid);
            Console.WriteLine(chart.text);

            chart.changeUid(uid);
            room.BroMsg(OpCode.CHART, ChartCode.SRES, chart, null);
        }

        public void OnDisconnect(clientPeer client) {
            
              
        }


        public bool isLegal(clientPeer client, int type) {

            int uid = users.getId(client);

            switch (type) {
                case 1: { //加入检查

                        if (!users.isOnline(uid)) {
                            //不在线
                            return false;
                        }
                        if (matchroom.isMatching(uid)) {
                            //若在等待队列,重复加入
                            return false;
                        }
                        break;
                    }
                case 2: {
                        //离开检查
                        if (!users.isOnline(uid)) {
                            //不在线
                            return false;
                        }
                        if (!matchroom.isMatching(uid)) {
                            //若不在等待队列
                            return false;
                        }
                        break;
                    }
                case 3: {
                        //断开检查
                        if (!users.isOnline(uid)) {
                            //不在线
                            return false;
                        }
                        if (!matchroom.isMatching(uid)) {
                            //重复加入
                            return false;
                        }
                        break;
                    }

            }



            return true;

        }


        public void Init() {
            throw new NotImplementedException();
        }
    }

