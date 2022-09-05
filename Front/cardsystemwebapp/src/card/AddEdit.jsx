import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';

import { cardService, alertService } from '@/_services';

function AddEdit({ history, match }) {
    const { id } = match.params;
    const isAddMode = !id;
    
    const initialValues = {
        Name:'',
        Balance: '',
        cardType: ''
    };

    const validationSchema = Yup.object().shape({
        name: Yup.string()
            .required('First Name is required'),
        balance: Yup.string()
            .required('Balance is required'),
        cardType: Yup.string()
            .required('card Type is required')
    });

    function onSubmit(fields, { setStatus, setSubmitting }) {
        setStatus();
        if (isAddMode) {
            createcard(fields, setSubmitting);
        } else {
            updatecard(id, fields, setSubmitting);
        }
    }

    function createcard(fields, setSubmitting) {
        cardService.create(fields)
            .then(() => {
                alertService.success('card added successfully', { keepAfterRouteChange: true });
                history.push('.');
            })
            .catch(error => {
                setSubmitting(false);
                alertService.error(error);
            });
    }

    function updatecard(id, fields, setSubmitting) {
        accuntService.update(id, fields)
            .then(() => {
                alertService.success('Update successful', { keepAfterRouteChange: true });
                history.push('..');
            })
            .catch(error => {
                setSubmitting(false);
                alertService.error(error);
            });
    }

    return (
        <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit}>
            {({ errors, touched, isSubmitting, setFieldValue }) => {
                useEffect(() => {
                    if (!isAddMode) {
                        // get card and set form fields
                        cardService.getById(id).then(card => {
                            const fields = ['name', 'cardNumber'];
                            fields.forEach(field => setFieldValue(field, card[field], false));
                        });
                    }
                }, []);

                return (
                    <Form>
                        <h1>{isAddMode ? 'Add card' : 'Edit card'}</h1>
                        <div className="form-row">
                            <div className="form-group col-7">
                                <label>Name</label>
                                <Field name="name" type="text" className={'form-control' + (errors.name && touched.name ? ' is-invalid' : '')} />
                                <ErrorMessage name="name" component="div" className="invalid-feedback" />
                            </div>
                        </div>
                        <div className="form-row">
                            <div className="form-group col-7">
                                <label>Card Number</label>
                                <Field name="cardNumber" type="text" className={'form-control' + (errors.cardType && touched.cardType ? ' is-invalid' : '')} />
                                <ErrorMessage name="cardNumber" component="div" className="invalid-feedback" />
                            </div>
                        </div>
                        <div className="form-group">
                            <button type="submit" disabled={isSubmitting} className="btn btn-primary">
                                {isSubmitting && <span className="spinner-border spinner-border-sm mr-1"></span>}
                                Save
                            </button>
                            <Link to={isAddMode ? '.' : '..'} className="btn btn-link">Cancel</Link>
                        </div>
                    </Form>
                );
            }}
        </Formik>
    );
}

export { AddEdit };