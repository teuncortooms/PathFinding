import React, { Component } from 'react';
import Node from "./Node/Node.jsx";
import { dijkstra } from "../../algorithms/dijkstra.js";

import "./PathFindingVisualiser.css";


export class PathFindingVisualiser extends Component {
    static displayName = PathFindingVisualiser.name;

    NUMBER_OF_ROWS = 20;
    NUMBER_OF_COLUMNS = 40;
    START_NODE_ROW = 10;
    START_NODE_COL = 5;
    FINISH_NODE_ROW = 10;
    FINISH_NODE_COL = 38;


    constructor(props) {
        super(props);
        this.state = {
            grid: [],
        };
    }

    componentDidMount() {
        const grid = this.getInitialGrid();
        this.setState({ grid });
    }

    animateDijkstra(visitedNodesInOrder) {
        for (let i = 0; i < visitedNodesInOrder.length; i++) {
            setTimeout(() => {
                const node = visitedNodesInOrder[i];
                const newGrid = this.state.grid.slice();
                const newNode = {
                    ...node,
                    isVisited: true,
                };
                newGrid[node.row][node.col] = newNode;
                this.setState({ grid: newGrid });
            }, 1000);
        }
    }

    visualiseDijkstra() {
        const { grid } = this.state;
        const startNode = grid[this.START_NODE_ROW][this.START_NODE_COL];
        const finishNode = grid[this.FINISH_NODE_ROW][this.FINISH_NODE_COL];
        const visitedNodesInOrder = dijkstra(grid, startNode, finishNode);
        this.animateDijkstra(visitedNodesInOrder);
    }

    getInitialGrid() {
        let grid = [];
        let nRows = this.NUMBER_OF_ROWS;
        let nCols = this.NUMBER_OF_COLUMNS;
        for (let iRow = 0; iRow < nRows; iRow++) {
            let row = [];
            for (let iCol = 0; iCol < nCols; iCol++) {
                row.push(this.createNode(iCol, iRow));
            }
            grid.push(row);
        }
        return grid;
    }

    createNode(iCol, iRow) {
        return {
            key: "r" + iRow + "c" + iCol,
            col: iCol,
            row: iRow,
            isStart: iRow === this.START_NODE_ROW && iCol === this.START_NODE_COL,
            isFinish: iRow === this.FINISH_NODE_ROW && iCol === this.FINISH_NODE_COL,
            distance: Infinity,
            isVisited: false,
            isWall: false,
            previousNode: null,
        };
    }

    render() {
        const { grid } = this.state;
        console.log(grid);

        return (
            <>
                <button onClick={() => this.visualiseDijkstra()}>
                    Visualise Dijkstra's Algorithm
        </button>
                <div className="grid">
                    {grid.map((row) => {
                        return (
                            <div>
                                {row.map((node) => {
                                    return (
                                        <Node
                                            key={node.key}
                                            isStart={node.isStart}
                                            isFinish={node.isFinish}
                                            isVisited={node.isVisited}
                                        ></Node>
                                    );
                                })}
                            </div>
                        );
                    })}
                </div>
            </>
        );
    }
}
