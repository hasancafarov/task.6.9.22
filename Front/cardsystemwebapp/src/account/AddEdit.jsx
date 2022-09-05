import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';

import { accountService, alertService } from '@/_services';

function AddEdit({ history, match }) {
    const { id } = match.params;
    const isAddMode = !id;
    
    const initialValues = {
        Name:'',
        Balance: '',
        AccountType: ''
    };

    const validationSchema = Yup.object().shape({
        name: Yup.string()
            .required('First Name is required'),
        balance: Yup.string()
            .required('Balance is required'),
        accountType: Yup.string()
            .email('Account Type is invalid')
            .required('Account Type is required')
    });

    function onSubmit(fields, { setStatus, setSubmitting }) {
        setStatus();
        if (isAddMode) {
            createAccount(fields, setSubmitting);
        } else {
            updateAccount(id, fields, setSubmitting);
        }
    }

    function createAccount(fields, setSubmitting) {
        accountService.create(fields)
            .then(() => {
                alertService.success('Account added successfully', { keepAfterRouteChange: true });
                history.push('.');
            })
            .catch(error => {
                setSubmitting(false);
                alertService.error(error);
            });
    }

    function updateAccount(id, fields, setSubmitting) {
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
                        accountService.getById(id).then(account => {
                            const fields = ['name', 'balance', 'accountType'];
                            fields.forEach(field => setFieldValue(field, account[field], false));
                        });
                    }
                }, []);

                return (
                    <Form>
                        <h1>{isAddMode ? 'Add Account' : 'Edit Account'}</h1>
                        <div className="form-row">
                            <div className="form-group col-7">
                                <label>Name</label>
                                <Field name="name" type="text" className={'form-control' + (errors.name && touched.name ? ' is-invalid' : '')} />
                                <ErrorMessage name="name" component="div" className="invalid-feedback" />
                            </div>
                        </div>
                        <div className="form-row">
                            <div className="form-group col-7">
                                <label>Balance</label>
                                <Field name="balance" type="text" className={'form-control' + (errors.balance && touched.balance ? ' is-invalid' : '')} />
                                <ErrorMessage name="balance" component="div" className="invalid-feedback" />
                            </div>
                        </div>
                        <div className="form-row">
                            <div className="form-group col-7">
                                <label>Account Type</label>
                                <Field name="accountType" type="text" className={'form-control' + (errors.accountType && touched.accountType ? ' is-invalid' : '')} />
                                <ErrorMessage name="accountType" component="div" className="invalid-feedback" />
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