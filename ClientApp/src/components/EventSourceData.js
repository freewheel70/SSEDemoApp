import {useState} from "react";

function EventSourceData() {

    const [status, setStatus] = useState("Unknown")
    const [list, setList] = useState([]);

    let onConnectSSEAPI = () => {
        setList([]);
        const evtSource = new EventSource("sse");

        evtSource.addEventListener("city-notification", event => {
            //debugger;
            setList(d => [...d, event.data]);
        });

        evtSource.addEventListener("open", event => {
            console.log("open: " + performance.now())
            setStatus("Open")
        })
        evtSource.addEventListener("error", (event) => {
            console.log("Error happened");
            console.log("error: " + performance.now())
            evtSource.close();
            setTimeout(() => onConnectSSEAPI(), Math.random() * 10000 + 5000)
        });

        evtSource.addEventListener("close", (event) => {
            evtSource.close();
            setStatus("Close")
            console.log("close: " + performance.now())
        });

    };
    return (
        <div style={{display: "flex", flexDirection: "row"}}>
            <div style={{flex: 1}}>
                <button className="btn btn-primary" onClick={onConnectSSEAPI}>
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
