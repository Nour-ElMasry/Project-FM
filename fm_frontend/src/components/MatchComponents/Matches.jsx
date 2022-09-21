import React, {useEffect, useState}  from 'react';
import GameweekMatchPagination from './GameweekMatchPagination';
import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';
import GeneralAxiosService from '../../services/GeneralAxiosService';
import LeagueMatchesTable from './LeagueMatchesTable';
import _ from 'lodash';
import { width } from '@mui/system';

const Matches = () => {
    const [pageApi, setPageApi] = useState(1);
    const [apiData, setApiData] = useState({});
    const [hasError, setHasError] = useState(false);
    const [matches, setMatches] = useState({});
    const [loading, setLoading] = useState(true);

    var groupByLeagueName = (list) => {
        return list.reduce((a, b) => {
          (a[b['fixtureLeague'].leagueName] = a[b['fixtureLeague'].leagueName] || []).push(b);
          return a;
        }, {});
    };    

    useEffect(() => {
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Fixtures/All/" + pageApi)
        .then(res => {
            setApiData(res.data);
            setMatches(groupByLeagueName(res.data.pageResults));
        })
        .then(() => setLoading(false))
        .catch((e) => {
            setLoading(false);
            setHasError(true);
        });
    },[pageApi]);

    const TablesDisplay = () => {
        
        if(loading === false){
            if (hasError) {
                return <h2 className='errorMsg'>Oops! Something went wrong!</h2>;
            }
            return <>
                <div className='gameweekTables'>
                    {_.map(matches, (x, i) => {
                        return <LeagueMatchesTable league={x} key={i}/>
                    })}
                </div> 
            </>
        }

        return <Box sx={{textAlign: "center", marginTop: "10rem"}}>
            <CircularProgress style={ {width: "3rem", height: "3rem"}} />
        </Box>
    }

    return <section className='matchSection container container--pa'> 
        <h1 className='title'>Matches From All Leagues</h1>
        {!hasError && <GameweekMatchPagination page={pageApi} setPage={setPageApi} maxPages={apiData.totalPages} setPageLoading={setLoading}/>}
        {TablesDisplay()}

    </section>
}

export default Matches;