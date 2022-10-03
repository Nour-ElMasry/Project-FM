import PropTypes from 'prop-types';
import CircularProgress from '@mui/material/CircularProgress';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';

RatingProgress.propTypes = {
    value: PropTypes.number.isRequired,
  };

function RatingProgress(props) {
  return (
    <Box sx={{ position: 'relative', display: 'block' }}>
      <CircularProgress size="3rem" variant="determinate" {...props} />
      <Box
        sx={{
          top: 15,
          left: 0,
          bottom:0,
          right: 0,
          position: 'absolute',
          textAlign: 'center',
        }}
      >
        <Typography variant="caption" component="div" color="text.secondary">
          {`${Math.round(props.value)}`}
        </Typography>
        {props.title && <Typography 
        sx={{marginTop: '1rem'}}
        variant="caption" component="div" color="text.secondary">
          {props.title}
        </Typography>}
      </Box>
    </Box>
  );
}

export default RatingProgress;