import React, {useEffect, useState}  from 'react';
import GameweekMatchPagination from './GameweekMatchPagination';
import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';
import GeneralAxiosService from '../../services/GeneralAxiosService';
import LeagueMatchesTable from './LeagueMatchesTable';
import _ from 'lodash';

const Matches = () => {
    const [pageApi, setPageApi] = useState(1);
    const [apiData, setApiData] = useState({});
    const [matches, setMatches] = useState({});
    const [loading, setLoading] = useState(true);

    var groupByLeagueName = (list) => {
        console.log(list);
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
        .then(() => setLoading(false));
    },[pageApi]);

    const TablesDisplay = () => {
        
        if(loading === false){
            return <>
                {_.map(matches, (x, i) => {
                    return <LeagueMatchesTable league={x} key={i}/>
                })}
            </> 
        }

        return <Box>
            <CircularProgress />
        </Box>
    }

    return <section className='matchSection container container--pa'> 
        <h1 className='title'>Matches From All Leagues</h1>
        <GameweekMatchPagination page={pageApi} setPage={setPageApi} maxPages={apiData.totalPages} setPageLoading={setLoading}/>
        <div className='gameweekTables'>
            {TablesDisplay()}
        </div>
    </section>
}

export default Matches;