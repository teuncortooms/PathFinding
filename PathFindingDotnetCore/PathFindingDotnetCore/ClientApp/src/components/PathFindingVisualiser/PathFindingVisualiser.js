import React, { Component } from "react";
import Node from "./Node/Node.js";
import { DIJKSTRA_API_URL } from "../../constants";
import "./PathFindingVisualiser.css";


class PathFindingVisualiser extends Component {
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
            mouseIsPressed: false
        };
    }

    componentDidMount() {
        const grid = this.getInitialGrid();
        this.setState({ grid });
    }

    getInitialGrid() {
        const grid = [];
        const nRows = this.NUMBER_OF_ROWS;
        const nCols = this.NUMBER_OF_COLUMNS;
        for (let iRow = 0; iRow < nRows; iRow++) {
            const row = [];
            for (let iCol = 0; iCol < nCols; iCol++) {
                row.push(this.createNode(iCol, iRow));
            }
            grid.push(row);
        }
        return grid;
    }

    createNode(iCol, iRow) {
        return {
            col: iCol,
            row: iRow,
            isStart: iRow === this.START_NODE_ROW && iCol === this.START_NODE_COL,
            isFinish: iRow === this.FINISH_NODE_ROW && iCol === this.FINISH_NODE_COL,
            distance: Infinity,
            isVisited: false,
            previousNode: null,
            isWall: false
        };
    }

    handleMouseDown(row, col) {
        const newGrid = this.getNewGridWithWallToggled(this.state.grid, row, col);
        this.setState({ grid: newGrid, mouseIsPressed: true });
    }

    handleMouseEnter(row, col) {
        if (!this.state.mouseIsPressed) return;
        const newGrid = this.getNewGridWithWallToggled(this.state.grid, row, col);
        this.setState({ grid: newGrid });
    }

    handleMouseUp() {
        this.setState({ mouseIsPressed: false });
    }

    getNewGridWithWallToggled = (grid, row, col) => {
        const newGrid = grid.slice();
        const node = newGrid[row][col];
        const newNode = {
            ...node,
            isWall: !node.isWall,
        };
        newGrid[row][col] = newNode;
        return newGrid;
    };

    render() {
        const { grid, mouseIsPressed } = this.state;

        return (
            <div>
                <button onClick={() => this.startDijkstra()}>
                    Visualise Dijkstra's Algorithm
                </button>
                <div className="grid">
                    {grid.map((row, iRow) => {
                        return (
                            <div key={iRow}>
                                {row.map((node, iNode) => {
                                    const {
                                        row,
                                        col,
                                        isFinish,
                                        isStart,
                                        isWall
                                    } = node;
                                    return (
                                        <Node
                                            key={iNode}
                                            col={col}
                                            row={row}
                                            isFinish={isFinish}
                                            isStart={isStart}
                                            isWall={isWall}
                                            mouseIsPressed={mouseIsPressed}
                                            onMouseDown={(row, col) => this.handleMouseDown(row, col)}
                                            onMouseEnter={(row, col) => this.handleMouseEnter(row, col)}
                                            onMouseUp={() => this.handleMouseUp()}
                                        ></Node>
                                    );
                                })}
                            </div>
                        );
                    })}
                </div>
            </div>
        );
    }

    startDijkstra() {
        fetch(`${"https://localhost:44373/Dijkstra/analyse"}`, {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                nodes: this.state.grid
            })
        })
            .then(result => result.json())
            .then(analytics => {
                this.animateDijkstra(analytics);
            })
            .catch(err => console.log(err));
    }

    animateDijkstra(analytics) {
        const visitedNodesInOrder = analytics.visitedInOrder;
        const path = analytics.shortestPathToDest;

        for (let i = 1; i < visitedNodesInOrder.length - 1; i++) {
            setTimeout(() => {
                const node = visitedNodesInOrder[i];
                document.getElementById(`node-${node.row}-${node.col}`).className =
                    "node node-visited";
            }, 10 * i);
        }
        setTimeout(() => {
            this.animatePath(path);
        }, 10 * visitedNodesInOrder.length);
    }

    animatePath(path) {
        for (let i = path.length - 2; i > 0; i--) {
            setTimeout(() => {
                const node = path[i];
                document.getElementById(`node-${node.row}-${node.col}`).className =
                    "node node-path";
            }, 50 * i);
        }
    }
}
export default PathFindingVisualiser;