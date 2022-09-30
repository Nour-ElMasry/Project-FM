import {useEffect, useState}  from 'react';
import GameweekMatchPagination from './GameweekMatchPagination';
import GeneralAxiosService from '../../services/GeneralAxiosService';
import MatchesTable from './MatchesTable';
import _ from 'lodash';
import Loading from '../Loading';
import { useNavigate } from 'react-router-dom';

const getPageNumber = () => {
  if(sessionStorage && parseInt(sessionStorage.getItem("Page_Key")) > 0) {
    return parseInt(sessionStorage.getItem("Page_Key"));
  }
  return 1;
}

const Matches = () => {
    const [pageApi, setPageApi] = useState(getPageNumber());
    const [apiData, setApiData] = useState({});
    const [hasError, setHasError] = useState(false);
    const [matches, setMatches] = useState({});
    const [loading, setLoading] = useState(true);
    const [user] = useState(JSON.parse(localStorage.getItem("User")));
    const navigate = useNavigate();

    var groupByLeagueName = (list) => {
        return list.reduce((a, b) => {
          (a[b['fixtureLeague'].leagueName] = a[b['fixtureLeague'].leagueName] || []).push(b);
          return a;
        }, {});
    };    

    useEffect(() => {
        if(user == null){
            navigate("/login");
        }else{
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
        }
    },[pageApi, user, navigate]);

    const TablesDisplay = () => {
        
        if(loading === false){
            if (hasError) {
                return <h2 className='errorMsg'>Oops! Something went wrong!</h2>;
            }
            return <>
                <div className='gameweekTables'>
                    {_.map(matches, (x, i) => {
                        return <MatchesTable tableTitle={x[0].fixtureLeague.leagueName} fixtures={x} key={i}/>
                    })}
                </div> 
            </>
        }

        return <Loading/>
    }

    return <section className='matchSection container container--pa'> 
        <h1 className='title'>Matches From All Leagues</h1>
        {!hasError && <GameweekMatchPagination page={pageApi} setPage={setPageApi} maxPages={apiData.totalPages} pageLoading={loading} setPageLoading={setLoading}/>}
        {TablesDisplay()}

    </section>
}

export default Matches;