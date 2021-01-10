import React from 'react';
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';
import { GRIDS_API_URL } from '../../constants';
class RegistrationForm extends React.Component {
    state = {
        id: 0,
        cells: ''
    }
    componentDidMount() {
        if (this.props.grid) {
            const { id, nodes } = this.props.grid
            this.setState({ id, nodes });
        }
    }
    onChange = e => {
        this.setState({ [e.target.name]: e.target.value })
    }
    submitNew = e => {
        e.preventDefault();
        fetch(`${GRIDS_API_URL}`, {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                //nodes: this.state.nodes
                nodes: null
            })
        })
            .then(result => result.json())
            .then(grid => {
                this.props.addGridToState(grid);
                this.props.toggle();
            })
            .catch(err => console.log(err));
    }
    submitEdit = e => {
        e.preventDefault();
        fetch(`${GRIDS_API_URL}/${this.state.id}`, {
            method: 'put',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                nodes: this.state.cells
            })
        })
            .then(() => {
                this.props.toggle();
                this.props.updateGridIntoState(this.state.id);
            })
            .catch(err => console.log(err));
    }
    render() {
        return <Form onSubmit={this.props.grid ? this.submitEdit : this.submitNew}>
            <FormGroup>
                <Label for="name">Nodes:</Label>
                <Input type="text" name="nodes" onChange={this.onChange} value={this.state.cells === '' ? '' : this.state.cells} />
            </FormGroup>
            <Button>Send</Button>
        </Form>;
    }
}
export default RegistrationForm;