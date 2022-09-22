import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';
import GeneralAxiosService from '../../services/GeneralAxiosService';
import MatchItem from './MatchItem';
import MatchEvents from './MatchEvents';

const SingleMatch = () => {
    const params = useParams();
    const [match, setMatch] = useState({});
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Fixtures/"+params.id)
        .then((response) => setMatch(response.data))
        .then(() => setLoading(false));
    },[params.id])

    return <section className='singleMatchSection container container--pa'>
        <h1 className='title'>Match Details</h1>
        
       
            {loading ? <Box sx={{textAlign: "center", marginTop: "10rem"}}>
                <CircularProgress style={ {width: "3rem", height: "3rem"}} />
            </Box> : 
            <div className='flex flex-ai-c flex-jc-sa matchHeader'>
                <MatchItem match={match}/>
                <MatchEvents events={match.fixtureEvents}/>
            </div>}
     

    </section>
}

export default SingleMatch;