import React, { Component } from "react";
import "./Cell.css";

export default class Cell extends Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        const {
            col,
            row,
            id,
            isDestination,
            isStart,
            isWall,
            onMouseDown,
            onMouseEnter,
            onMouseUp
        } = this.props;
        const extraClassName = isWall ? "node-wall" : isDestination ? "node-destination" : isStart ? "node-start" : "";

        return (
            <div
                id={`node-${id}`}
                className={`node ${extraClassName}`}
                onMouseDown={() => onMouseDown(row, col)}
                onMouseEnter={() => onMouseEnter(row, col)}
                onMouseUp={() => onMouseUp()}
            ></div>
        );
    }
}
