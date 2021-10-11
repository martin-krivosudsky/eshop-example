import React, { Component } from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';

export class ProductDetailComponent extends Component {
    render() {
        return (
            <Modal show={this.props.isOpen} onHide={this.props.handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>{this.props.product?.name}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <b>Description:</b><br/>
                    <input
                        type="text"
                        id="description"
                        value={this.props.product?.description ?? ''}
                        name="description"
                        onChange={this.props.changeDescription}
                    />
                    <br />
                    <b>Price:</b> {this.props.product?.price}
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={this.props.handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={this.props.handleSave}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>
        );
    }
}