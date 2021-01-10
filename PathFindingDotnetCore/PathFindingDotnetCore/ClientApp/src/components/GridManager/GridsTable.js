import React, { Component } from 'react';
import { Table, Button } from 'reactstrap';
import { Link } from 'react-router-dom';
import RegistrationModal from './EditGridModal';
import { GRIDS_API_URL } from '../../constants';
class GridsTable extends Component {
    deleteGrid = id => {
        let confirmDeletion = window.confirm('Do you really wish to delete it?');
        if (confirmDeletion) {
            fetch(`${GRIDS_API_URL}/${id}`, {
                method: 'delete',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(res => {
                    this.props.deleteGridFromState(id);
                })
                .catch(err => console.log(err));
        }
    }
    render() {
        const grids = this.props.grids;
        return <Table striped>
            <thead className="thead-dark">
                <tr>
                    <th>Id</th>
                    <th>Size</th>
                    <th style={{ textAlign: "center" }}>Actions</th>
                </tr>
            </thead>
            <tbody>
                {!grids || grids.length <= 0 ?
                    <tr>
                        <td colSpan="6" align="center"><b>No grids yet</b></td>
                    </tr>
                    : grids.map(grid => (
                        <tr key={grid.id}>
                            <th scope="row">
                                {grid.id}
                            </th>
                            <td>
                                {grid.cells[0].length} * {grid.cells.length}
                            </td>
                            <td align="center">
                                <div>
                                    {
                                        //<RegistrationModal
                                        //    isNew={false}
                                        //    grid={grid}
                                        //    updateGridIntoState={this.props.updateState} />
                                    }
                                    <Button color="warning" tag={Link} to={{
                                        pathname: "/Visualiser",
                                        state: { grid: grid.cells }
                                    }}>Visualiser</Button>

                                    &nbsp;&nbsp;&nbsp;

                                    <Button color="danger" onClick={
                                        () => this.deleteGrid(grid.id)
                                    }>Delete</Button>
                                </div>
                            </td>
                        </tr>
                    ))}
            </tbody>
        </Table >;
    }
}
export default GridsTable;