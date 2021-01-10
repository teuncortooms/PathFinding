import React, { Component } from 'react';
import { Col, Container, Row } from 'reactstrap';
import { GRIDS_API_URL } from '../../constants';
import GridsTable from './GridsTable';
import RegistrationModal from './EditGridModal';

class Home extends Component {
    state = {
        grids: []
    }
    componentDidMount() {
        this.getGrids();
    }
    getGrids = () => {
        fetch(GRIDS_API_URL)
            .then(result => result.json())
            .then(result => this.setState({ grids: result }))
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
                    <h3>Grid management</h3>
                </Col>
            </Row>
            <Row>
                <Col>
                    <GridsTable
                        grids={this.state.grids}
                        updateState={this.updateState}
                        deleteGridFromState={this.deleteGridFromState} />
                </Col>
            </Row>
            {
            //<Row>
            //    <Col>
            //        <RegistrationModal isNew={true} addGridToState={this.addGridToState} />
            //    </Col>
            //</Row>
            }
        </Container>;
    }
}
export default Home;
