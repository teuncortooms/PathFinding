import React, { Component, Fragment } from 'react';
import { Button, Modal, ModalHeader, ModalBody } from 'reactstrap';
import PathFindingVisualiser from '../PathFindingVisualiser/PathFindingVisualiser';

class RegistrationModal extends Component {
    state = {
        modal: false
    }
    toggle = () => {
        this.setState(previous => ({
            modal: !previous.modal
        }));
    }
    render() {
        const isNew = this.props.isNew;
        let title = 'Edit Grid';
        let button = '';
        if (isNew) {
            title = 'Add Grid';
            button = <Button
                color="success"
                onClick={this.toggle}
                style={{ minWidth: "200px" }}>Add</Button>;
        } else {
            button = <Button
                color="warning"
                onClick={this.toggle}>Edit</Button>;
        }
        return <Fragment>
            {button}
            <Modal isOpen={this.state.modal} toggle={this.toggle} className={this.props.className}>
                <ModalHeader toggle={this.toggle}>{title}</ModalHeader>
                <ModalBody>
                    <PathFindingVisualiser
                        addGridToState={this.props.addGridToState}
                        updateGridIntoState={this.props.updateGridIntoState}
                        toggle={this.toggle}
                        grid={this.props.grid} />
                </ModalBody>
            </Modal>
        </Fragment>;
    }
}
export default RegistrationModal;