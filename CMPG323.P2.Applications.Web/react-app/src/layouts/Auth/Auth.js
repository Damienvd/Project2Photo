import React, { Suspense } from 'react';
import { useSelector } from 'react-redux';
import { renderRoutes } from 'react-router-config';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/styles';
import { LinearProgress } from '@material-ui/core';
import { Redirect } from 'react-router-dom';


const useStyles = makeStyles(theme => ({
  root: {
    height: '100%',
    width: '100%',
    display: 'flex',
    flexDirection: 'column',
    overflow: 'hidden'
  },
  content: {
    overflowY: 'auto',
    flex: '1 1 auto',
    paddingTop: '50px'
  }
}));

const Auth = props => {
  const { route } = props;

  const classes = useStyles();
  const { user } = useSelector(state => state.account);

  return (
    user ?
    <Redirect to='/home' /> :
    <div className={classes.root}>
      <main className={classes.content}>
        <Suspense fallback={<LinearProgress />}>
          {renderRoutes(route.routes)}
        </Suspense>
      </main>
    </div>
  );
};

Auth.propTypes = {
  route: PropTypes.object
};

export default Auth;
