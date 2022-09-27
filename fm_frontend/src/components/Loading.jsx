import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';

const Loading = () => {
    return <Box className="loading">
        <CircularProgress style={ {width: "3rem", height: "3rem"}} />
    </Box>
}

export default Loading;