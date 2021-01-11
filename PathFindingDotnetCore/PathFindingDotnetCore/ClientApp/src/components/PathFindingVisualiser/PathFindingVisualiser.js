import React, { Component } from "react";
import { Button, Form, FormGroup, Input, Label, Container, Row, Col } from 'reactstrap';
import { InputGroup, InputGroupAddon, InputGroupText } from 'reactstrap';
import Cell from "./Cell/Cell.js";
import * as urls from "../../constants";
import { dijkstraAnalyse, getDijkstraReport } from "../../algorithms/dijkstraGridAnalysis.js"
import "./PathFindingVisualiser.css";


class PathFindingVisualiser extends Component {
    static displayName = PathFindingVisualiser.name;

    GRID_WIDTH = 750;
    GRID_HEIGHT = 500;

    constructor(props) {
        super(props);

        this.state = {
            grid: [],
            numberOfCols: 45,
            numberOfRows: 30,
            startRow: 10,
            startCol: 5,
            destinationRow: 10,
            destinationCol: 28,
            speed: 3,
            mouseIsPressed: false
        };
    }

    componentDidMount() {
        const propsState = this.props.location.state;
        const apiGrid = propsState ? propsState.grid : null;
        const grid = apiGrid ? this.prepareGrid(apiGrid) : this.getInitialGrid();
        //const grid = this.getInitialGrid();
        this.setState({ grid });
    }

    prepareGrid(apiGrid) {
        return apiGrid.map(apiRow => {
            return apiRow.map(apiCell => {
                return {
                    id: apiCell.id,
                    col: apiCell.col,
                    row: apiCell.row,
                    isStart: apiCell.isStart,
                    isDestination: apiCell.isDestination,
                    isWall: apiCell.isWall,
                    distance: Infinity,
                    isVisited: false,
                    parent: null
                }
            })
        });
    }

    getInitialGrid() {
        const grid = [];
        const nRows = this.state.numberOfRows;
        const nCols = this.state.numberOfCols;
        let id = 1;
        for (let i = 0; i < nRows; i++) {
            const row = [];
            for (let j = 0; j < nCols; j++) {
                row.push(this.createCell(i, j, id));
                id++;
            }
            grid.push(row);
        }
        return grid;
    }

    createCell(row, col, id) {
        return {
            id: id,
            col: col,
            row: row,
            isStart: row === this.state.startRow && col === this.state.startCol,
            isDestination: row === this.state.destinationRow && col === this.state.destinationCol,
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
        const cell = newGrid[row][col];
        const newCell = {
            ...cell,
            isWall: !cell.isWall,
        };
        newGrid[row][col] = newCell;
        return newGrid;
    }

    setSpeed = e => {
        this.setState({ speed: parseInt(e.target.value) || 1 })
    }

    setRows = e => {
        const newRows = parseInt(e.target.value) || 0;
        this.state.numberOfRows = newRows;
        this.state.numberOfCols = newRows / this.GRID_HEIGHT * this.GRID_WIDTH;
        const grid = this.getInitialGrid();
        this.setState({ grid: grid })
    }

    setCols = e => {
        const newCols = parseInt(e.target.value) || 0;
        this.state.numberOfCols = newCols;
        this.state.numberOfRows = newCols / this.GRID_WIDTH * this.GRID_HEIGHT;
        const grid = this.getInitialGrid();
        this.setState({ grid: grid })
    }

    setStartRow = e => {
        this.state.startRow = parseInt(e.target.value) || 0;
        const grid = this.getInitialGrid();
        this.setState({ grid: grid })
    }

    setStartCol = e => {
        this.state.startCol = parseInt(e.target.value) || 0;
        const grid = this.getInitialGrid();
        this.setState({ grid: grid })
    }

    setDestRow = e => {
        this.state.destinationRow = parseInt(e.target.value) || 0;
        const grid = this.getInitialGrid();
        this.setState({ grid: grid })
    }

    setDestCol = e => {
        this.state.destinationCol = parseInt(e.target.value) || 0;
        const grid = this.getInitialGrid();
        this.setState({ grid: grid })
    }

    submitHandler(e) {
        e.preventDefault();
    }

    render() {
        const { grid, mouseIsPressed } = this.state;
        const cellHeight = this.GRID_HEIGHT / this.state.numberOfRows;
        const cellWidth = this.GRID_WIDTH / this.state.numberOfCols;

        return (
            <Container fluid={true}>
                <Row>
                    <Col>
                        <Form inline onSubmit={this.submitHandler}>
                            <Button className="m-1" color="info" onClick={() => this.startJSDijkstra()}>
                                Dijkstra JS
                            </Button>
                            <Button className="m-1" color="info" onClick={() => this.startAPIDijkstra()}>
                                Dijkstra API
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
                                <Input style={{ width: 50 }} type="text" name="speed" onChange={this.setSpeed} value={this.state.speed} />
                            </FormGroup>
                        </Form>
                    </Col>
                </Row>
                <Row>
                    <Col className="col-auto" >
                        <div className="grid" style={{ width: this.GRID_WIDTH, height: this.GRID_HEIGHT }} >
                            {grid.map((row, iRow) => {
                                return (
                                    <div key={iRow}>
                                        {row.map((cell) => {
                                            const {
                                                row,
                                                col,
                                                id,
                                                isDestination,
                                                isStart,
                                                isWall
                                            } = cell;
                                            return (
                                                <Cell
                                                    key={id}
                                                    height={cellHeight}
                                                    width={cellWidth}
                                                    id={id}
                                                    col={col}
                                                    row={row}
                                                    isDestination={isDestination}
                                                    isStart={isStart}
                                                    isWall={isWall}
                                                    mouseIsPressed={mouseIsPressed}
                                                    onMouseDown={(row, col) => this.handleMouseDown(row, col)}
                                                    onMouseEnter={(row, col) => this.handleMouseEnter(row, col)}
                                                    onMouseUp={() => this.handleMouseUp()}
                                                ></Cell>
                                            );
                                        })}
                                    </div>
                                );
                            })}
                        </div>
                    </Col>
                    <Col className="col-2">
                        <Form onSubmit={this.submitHandler}>
                            <FormGroup className="m-1">
                                <Label for="rows">Dimensions:</Label>
                                <InputGroup>
                                    <InputGroup>
                                        <InputGroupAddon addonType="prepend" style={{ minWidth: 60, textAlign: 'left' }}>
                                            <InputGroupText style={{ width: 100 + '%' }}>cols</InputGroupText>
                                        </InputGroupAddon>
                                        <Input onChange={this.setCols} value={this.state.numberOfCols} />
                                    </InputGroup>
                                    <InputGroupAddon addonType="prepend" style={{ minWidth: 60, textAlign: 'left' }}>
                                        <InputGroupText style={{ width: 100 + '%' }}>rows</InputGroupText>
                                    </InputGroupAddon>
                                    <Input onChange={this.setRows} value={this.state.numberOfRows} />
                                </InputGroup>
                            </FormGroup>
                            <FormGroup className="m-1">
                                <Label for="rows">Start:</Label>
                                <InputGroup>
                                    <InputGroupAddon addonType="prepend" style={{ minWidth: 60, textAlign: 'left' }}>
                                        <InputGroupText style={{ width: 100 + '%' }}>col</InputGroupText>
                                    </InputGroupAddon>
                                    <Input onChange={this.setStartCol} value={this.state.startCol} />
                                </InputGroup>
                                <InputGroup>
                                    <InputGroupAddon addonType="prepend" style={{ minWidth: 60, textAlign: 'left' }}>
                                        <InputGroupText style={{ width: 100 + '%' }}>row</InputGroupText>
                                    </InputGroupAddon>
                                    <Input onChange={this.setStartRow} value={this.state.startRow} />
                                </InputGroup>
                            </FormGroup>
                            <FormGroup className="m-1">
                                <Label for="rows">Destination:</Label>
                                <InputGroup>
                                    <InputGroupAddon addonType="prepend" style={{ minWidth: 60, textAlign: 'left' }}>
                                        <InputGroupText style={{ width: 100 + '%' }}>col</InputGroupText>
                                    </InputGroupAddon>
                                    <Input onChange={this.setDestCol} value={this.state.destinationCol} />
                                </InputGroup>
                                <InputGroup>
                                    <InputGroupAddon addonType="prepend" style={{ minWidth: 60, textAlign: 'left' }}>
                                        <InputGroupText style={{ width: 100 + '%' }}>row</InputGroupText>
                                    </InputGroupAddon>
                                    <Input onChange={this.setDestRow} value={this.state.destinationRow} />
                                </InputGroup>
                            </FormGroup>
                        </Form>
                    </Col>
                </Row>
            </Container>
        );
    }

    startJSDijkstra() {
        const { grid } = this.state;
        const visitedNodesInOrder = dijkstraAnalyse(grid);
        const dijkstraReport = getDijkstraReport(visitedNodesInOrder);
        this.animateAnalysis(dijkstraReport);
    }

    startAPIDijkstra() {
        this.startAPIAnalysis(urls.DIJKSTRA_API_URL);
    }

    startAPIAStar() {
        this.startAPIAnalysis(urls.ASTAR_API_URL);
    }

    startAPIAnalysis(API_URL) {
        const apiGrid = this.mapGridToAPIGrid();
        const jsonGrid = JSON.stringify(apiGrid);
        console.log(jsonGrid);


        fetch(API_URL, {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: jsonGrid
        })
            .then(result => result.json())
            .then(dijkstraReport => this.animateAnalysis(dijkstraReport))
            .catch(err => console.log(err));
    }

    mapGridToAPIGrid() {
        const grid = {};
        grid.cells = this.state.grid.map(row => {
            return row.map(node => {
                return {
                    id: node.id,
                    isStart: node.isStart,
                    isDestination: node.isDestination,
                    isWall: node.isWall
                };
            });
        });

        return grid;
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