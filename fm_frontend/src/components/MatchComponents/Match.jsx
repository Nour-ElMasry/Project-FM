import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import GeneralAxiosService from '../../services/GeneralAxiosService';
import MatchItem from './MatchItem';
import MatchEvents from './MatchEvents';
import MatchInfo from './MatchInfo';
import MatchTeamsLeagueTable from './MatchTeamsLeagueTable';
import Loading from '../Loading';

const Match = () => {
    const params = useParams();
    const [match, setMatch] = useState({});
    const [loading, setLoading] = useState(true);
    const [hasError, setHasError] = useState(false);

    useEffect(() => {
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Fixtures/"+params.id)
        .then((response) => setMatch(response.data))
        .then(() => setLoading(false))
        .catch((e) => {
            setLoading(false);
            setHasError(true);
        });
    },[params.id])

    return <section className='singleMatchSection container container--pa'>
        <h1 className='title'>Match Details</h1>

            {loading && <Loading />}
            {hasError && <h2 className='errorMsg'>Oops! Something went wrong!</h2>}
            {(!hasError && !loading) && <>
                <div className='matchesContainer container--styled matchHeader flex flex-ai-c flex-jc-sa '>
                    <MatchItem tableView={false} match={match}/>
                </div>
                {match.isPlayed && <div className='matchesContainer container--styled eventContainer'>
                    <MatchEvents match={match}/>
                </div>}
                <div className='infoAndLeagueContainer'>
                <MatchTeamsLeagueTable league={match.fixtureLeague} homeTeam={match.homeTeam} awayTeam={match.awayTeam}/>
                    <div className='matchesContainer container--styled matchInfo'>
                        <MatchInfo match={match}/>
                    </div>
                </div>
            </>}
     

    </section>
}

export default Match;