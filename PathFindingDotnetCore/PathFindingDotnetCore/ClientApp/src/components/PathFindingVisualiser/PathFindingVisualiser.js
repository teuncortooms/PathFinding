import React, { Component } from "react";
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';
import Node from "./Node/Node.js";
import * as urls from "../../constants";
import { dijkstraAnalyse, getDijkstraReport } from "../../algorithms/dijkstra.js"
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
            speed: 3,
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
        let id = 1;
        for (let iRow = 0; iRow < nRows; iRow++) {
            const row = [];
            for (let iCol = 0; iCol < nCols; iCol++) {
                row.push(this.createNode(iCol, iRow, id));
                id++;
            }
            grid.push(row);
        }
        return grid;
    }

    createNode(iCol, iRow, id) {
        return {
            id: id,
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
    }

    setSpeed = e => {
        this.setState({ speed: e.target.value })
    }

    render() {
        const { grid, mouseIsPressed } = this.state;

        return (
            <div>
                <Form inline>
                    <Button className="m-1" color="info" onClick={() => this.startJSDijkstra()}>
                        Dijkstra JS
                    </Button>
                    <Button className="m-1" color="info" onClick={() => this.startAPIDijkstra_v1()}>
                        Dijkstra API v1
                    </Button>
                    <Button className="m-1" color="info" onClick={() => this.startAPIDijkstra_v2()}>
                        Dijkstra API v2
                    </Button>
                    <Button className="m-1" color="success" onClick={() => this.startAPIAStar()}>
                        A* API
                    </Button>
                    <Button className="m-1" onClick={() => this.resetAnalysis()}>
                        Clear Analysis
                    </Button>
                    <Button className="m-1" onClick={() => this.resetAll()}>
                        Reset All
                    </Button>
                    <FormGroup className="m-1">
                        <Label for="speed">Speed:</Label>
                        <Input className="col-3" type="text" name="speed" onChange={this.setSpeed} value={this.state.speed} />
                    </FormGroup>
                </Form>

                <div className="grid">
                    {grid.map((row, iRow) => {
                        return (
                            <div key={iRow}>
                                {row.map((node, iNode) => {
                                    const {
                                        row,
                                        col,
                                        id,
                                        isFinish,
                                        isStart,
                                        isWall
                                    } = node;
                                    return (
                                        <Node
                                            key={iNode}
                                            id={id}
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

    startJSDijkstra() {
        const { grid } = this.state;
        const startNode = grid[this.START_NODE_ROW][this.START_NODE_COL];
        const finishNode = grid[this.FINISH_NODE_ROW][this.FINISH_NODE_COL];
        const visitedNodesInOrder = dijkstraAnalyse(grid, startNode, finishNode);
        const dijkstraReport = getDijkstraReport(visitedNodesInOrder);
        this.animateAnalysis(dijkstraReport);
    }

    startAPIDijkstra_v1() {
        this.startAPIAnalysis(urls.DIJKSTRA_V1_API_URL);
    }

    startAPIDijkstra_v2() {
        this.startAPIAnalysis(urls.DIJKSTRA_V2_API_URL);
    }

    startAPIAStar() {
        this.startAPIAnalysis(urls.ASTAR_API_URL);
    }

    startAPIAnalysis(API_URL) {
        const apiData = this.mapGridToAPIGrid();
        const json = JSON.stringify({
            nodes: apiData
        })
        console.log(json);


        fetch(API_URL, {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: json
        })
            .then(result => result.json())
            .then(dijkstraReport => this.animateAnalysis(dijkstraReport))
            .catch(err => console.log(err));
    }

    mapGridToAPIGrid() {
        const apiData = [];
        this.state.grid.map(row => {
            const apiRow = [];
            row.map(node => {
                apiRow.push(
                    {
                        id: node.id,
                        isStart: node.isStart,
                        isFinish: node.isFinish,
                        isWall: node.isWall
                    }
                )
            });
            apiData.push(apiRow);
        });
        return apiData;
    }

    animateAnalysis(dijkstraReport) {
        this.resetAnalysis();

        console.log(dijkstraReport);
        const visitedNodesInOrder = dijkstraReport.visitedInOrder;
        const path = dijkstraReport.shortestPathToDest;

        for (let i = 1; i < visitedNodesInOrder.length - 1; i++) {
            setTimeout(() => {
                const nodeId = visitedNodesInOrder[i];
                if (nodeId > 0)
                    document.getElementById(`node-${nodeId}`).className = // non-react hack
                        "node node-visited";
                if (nodeId < 0)
                    document.getElementById(`node-${Math.abs(nodeId)}`).classList.remove("node-visited");
            }, 10 / this.state.speed * i);
        }
        setTimeout(() => {
            this.animatePath(path);
        }, 10 / this.state.speed * visitedNodesInOrder.length);
    }

    animatePath(path) {
        for (let i = path.length - 2; i > 0; i--) {
            setTimeout(() => {
                const nodeId = path[i];
                document.getElementById(`node-${nodeId}`).className =
                    "node node-path";
            }, 50 / this.state.speed * i);
        }
    }

    resetAnalysis() {
        const grid = this.state.grid;
        for (let iRow = 0; iRow < grid.length; iRow++) { // hack to undo non-react hack above
            let row = grid[iRow];
            for (let iCol = 0; iCol < row.length; iCol++) {
                const nodeId = grid[iRow][iCol].id;
                document.getElementById(`node-${nodeId}`).classList.remove("node-visited", "node-path");
            }
        }
    }

    resetAll() {
        const grid = this.getInitialGrid();
        this.resetAnalysis();
        this.setState({ grid });
    }
}
export default PathFindingVisualiser;