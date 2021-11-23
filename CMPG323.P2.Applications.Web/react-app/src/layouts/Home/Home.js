import React, { Suspense, Fragment } from 'react';
import { useSelector } from 'react-redux';
import { renderRoutes } from 'react-router-config';
import { 
  LinearProgress
} from '@material-ui/core';
import { makeStyles } from '@material-ui/styles';
import PropTypes from 'prop-types';

import { Redirect } from 'react-router-dom';

const useStyles = makeStyles(theme => ({
  root: {
    height: '100%',
    width: '100%',
    display: 'flex',
    flexDirection: 'column',
    overflow: 'hidden'
  },
  container: {
    display: 'flex',
    flex: '1 1 auto',
    overflow: 'hidden'
  },
  content: {
    overflowY: 'auto',
    flex: '1 1 auto'
  }
}));

const Home = props => {
  const { route } = props;

  const classes = useStyles();
  const { user } = useSelector(state => state.account);

  return (
    user ?
    <Fragment>
      <div className={classes.root}>
        <div className={classes.container}>
          <main className={classes.content}>
            <Suspense fallback={<LinearProgress />}>
              {renderRoutes(route.routes)}
            </Suspense>
          </main>
        </div>
      </div>
    </Fragment> :
    <Redirect to='/auth/login' />
  )
}

Home.propTypes = {
  route: PropTypes.object
};

export default Home;