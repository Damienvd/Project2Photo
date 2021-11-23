import React from 'react';
import { Helmet } from 'react-helmet';
import PropTypes from 'prop-types';

const Page = props => {
  const { title, children, className } = props;
  return (
    <div className={className}>
      <Helmet>
        <title>{title}</title>
      </Helmet>
      {children}
    </div>
  );
};

Page.propTypes = {
  children: PropTypes.node,
  title: PropTypes.string,
  className: PropTypes.string
};

export default Page;
