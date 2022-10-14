const PlayerHeader = (props) => {
    return <div className="globalHeaderInfo">
        <img 
            className="globalImg" 
            src={props.player?.playerPerson?.image} 
            alt="Player"
        />
        <div className="globalName">
            <h2>{props.player?.playerPerson?.name}</h2>
            <p>{props.player?.position}</p>
        </div>
    </div>
}

export default PlayerHeader;

