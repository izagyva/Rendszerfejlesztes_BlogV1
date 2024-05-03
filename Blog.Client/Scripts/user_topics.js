import { getData } from './api.js';

async function onLoadUserTopics() {
    const topics = await getUserTopics();
    const topicListElement = document.getElementById('user-topic-list');
    topics.forEach(topic => {
        const topicElement = document.createElement('div');
        topicElement.textContent = topic.title; // replace with your actual topic title property
        topicListElement.appendChild(topicElement);
    });
}

async function getUserTopics() {
    const response = await getData('topics/user');
    if (response.status === 200) {
        return response.json();
    } else {
        console.error('Error getting user topics:', response);
    }
}

export { getUserTopics };
