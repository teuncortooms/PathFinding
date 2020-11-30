import React, { Component } from "react";
import "./Node.css";

export default class Node extends Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        if (this.props.isStart) return (<div className="node node-start"></div>);
        if (this.props.isFinish) return (<div className="node node-finish"></div>);
        if (this.props.isVisited) return (<div className="node node-visited"></div>);
        else return <div className="node"></div>
    }
}
