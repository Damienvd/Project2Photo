import React from 'react';
import { useDispatch } from 'react-redux';
import useRouter from 'utils/useRouter';
import { logout } from 'actions/accountActions';
import {
  Button,
} from '@material-ui/core';
import { Page } from '../../components';

const Home = () => {

  const { history } = useRouter();
  const dispatch = useDispatch();

  const handleLogout = async () => {
    await dispatch(logout());
    history.push('/');
  };

  return (
    <Page title='Home'>

      



      <Button
        onClick={handleLogout}
      >
        Sign out
      </Button>
    </Page>
  )
}

export default Home;
