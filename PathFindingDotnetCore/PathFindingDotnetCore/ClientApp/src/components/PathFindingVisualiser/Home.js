import React, { Component } from 'react';
import { Col, Container, Row } from 'reactstrap';
import PathFindingVisualiser from './PathFindingVisualiser';
import { GRIDS_API_URL } from '../constants';

class Home extends Component {
    state = {
        grids: []
    }
    componentDidMount() {
        this.getGrids();
    }
    getGrids = () => {
        fetch(GRIDS_API_URL)
            .then(res => res.json())
            .then(res => this.setState({ grids: res }))
            .catch(err => console.log(err));
    }
    addGridToState = grid => {
        this.setState(previous => ({
            grids: [...previous.grids, grid]
        }));
    }
    updateState = (id) => {
        this.getGrids();
    }
    deleteGridFromState = id => {
        const updated = this.state.grids.filter(grid => grid.id !== id);
        this.setState({ grids: updated })
    }
    render() {
        return <Container style={{ paddingTop: "100px" }}>
            <Row>
                <Col>
                    <h3>My First React + ASP.NET CRUD React</h3>
                </Col>
            </Row>
            <Row>
                <Col>
                    <DataTable
                        grids={this.state.grids}
                        updateState={this.updateState}
                        deleteGridFromState={this.deleteGridFromState} />
                </Col>
            </Row>
            <Row>
                <Col>
                    <RegistrationModal isNew={true} addGridToState={this.addGridToState} />
                </Col>
            </Row>
        </Container>;
    }
}
export default Home;
