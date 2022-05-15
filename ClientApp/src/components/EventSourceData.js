import {useState} from "react";

function EventSourceData() {

    const [status, setStatus] = useState("Unknown")
    const [list, setList] = useState([]);

    return (
        <div style={{display: "flex", flexDirection: "row"}}>
            <div style={{flex: 1}}>
                <button className="btn btn-primary" onClick={() => {
                    setList([]);
                    const evtSource = new EventSource("sse");
                    evtSource.addEventListener("open", event=>{
                        setStatus("Open")
                    })
                    console.log("new event source connected")
                    evtSource.addEventListener("data", event => {
                        console.log("list length: " + list.length)
                        console.log("event.data " + event.data);
                        //debugger;
                        setList(d => [...d, event.data]);
                    });
                    evtSource.addEventListener("close", (event) => {
                        console.log("Close connection");
                        evtSource.close();
                        setStatus("Close")
                    });
                }}>
                    Start Receive Server Events
                </button>
                <br/>
                <br/>
                <label>Status: {status}</label>
            </div>
            <ul style={{flex: 3}}>
                {list.length > 0 && <h4 style={{marginLeft: '-20px'}}>Cities</h4>}
                {list.map((item, index) => <li key={index}>{item}</li>)}
            </ul>
        </div>
    );
}

export default EventSourceData;
