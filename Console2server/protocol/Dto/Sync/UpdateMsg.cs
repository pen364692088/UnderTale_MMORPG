using System;
using System.Collections.Generic;
[Serializable]
public class UpdateMsg {
    public int frameCount = 0;
    public List<Record> records = new List<Record>();
    public UpdateMsg() {

    }
}