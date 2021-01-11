import React, { Component } from "react";
import { Col } from 'reactstrap';
import "./Cell.css";

export default class Cell extends Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        const {
            height,
            width,
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
            <div style={{ width: width, height: height }}
                id={`node-${id}`}
                className={`node ${extraClassName}`}
                onMouseDown={() => onMouseDown(row, col)}
                onMouseEnter={() => onMouseEnter(row, col)}
                onMouseUp={() => onMouseUp()}
            ></div>
        );
    }
}
