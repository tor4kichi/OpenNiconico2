using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
/* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */

namespace Mntone.Nico2.Users.Video
{
	[XmlRoot(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
	public class Link
	{
		[XmlAttribute(AttributeName = "rel")]
		public string Rel { get; set; }
		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }
		[XmlAttribute(AttributeName = "href")]
		public string Href { get; set; }
	}

	[XmlRoot(ElementName = "guid")]
	public class Guid
	{
		[XmlAttribute(AttributeName = "isPermaLink")]
		public string IsPermaLink { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "thumbnail", Namespace = "http://search.yahoo.com/mrss/")]
	public class Thumbnail
	{
		[XmlAttribute(AttributeName = "url")]
		public string Url { get; set; }
		[XmlAttribute(AttributeName = "width")]
		public string Width { get; set; }
		[XmlAttribute(AttributeName = "height")]
		public string Height { get; set; }
	}

	[XmlRoot(ElementName = "item")]
	public class Item
	{
		[XmlElement(ElementName = "title")]
		public string Title { get; set; }
		[XmlElement(ElementName = "link")]
		public string Link2 { get; set; }
		[XmlElement(ElementName = "guid")]
		public Guid Guid { get; set; }
		[XmlElement(ElementName = "pubDate")]
		public string PubDate { get; set; }
		[XmlElement(ElementName = "description")]
		public string Description { get; set; }
		[XmlElement(ElementName = "thumbnail", Namespace = "http://search.yahoo.com/mrss/")]
		public Thumbnail Thumbnail { get; set; }
		[XmlElement(ElementName = "verb", Namespace = "http://activitystrea.ms/spec/1.0/")]
		public string Verb { get; set; }
		[XmlElement(ElementName = "object-type", Namespace = "http://activitystrea.ms/spec/1.0/")]
		public List<string> Objecttype { get; set; }
	}

	[XmlRoot(ElementName = "channel")]
	public class Channel
	{
		[XmlElement(ElementName = "title")]
		public string Title { get; set; }
		[XmlElement(ElementName = "link")]
		public string Link { get; set; }
		[XmlElement(ElementName = "description")]
		public string Description { get; set; }
		[XmlElement(ElementName = "pubDate")]
		public string PubDate { get; set; }
		[XmlElement(ElementName = "lastBuildDate")]
		public string LastBuildDate { get; set; }
		[XmlElement(ElementName = "generator")]
		public string Generator { get; set; }
		[XmlElement(ElementName = "creator", Namespace = "http://purl.org/dc/elements/1.1/")]
		public string Creator { get; set; }
		[XmlElement(ElementName = "language")]
		public string Language { get; set; }
		[XmlElement(ElementName = "copyright")]
		public string Copyright { get; set; }
		[XmlElement(ElementName = "docs")]
		public string Docs { get; set; }
		[XmlElement(ElementName = "item")]
		public List<Item> Item { get; set; }
	}

	[XmlRoot(ElementName = "rss")]
	public class UserVideoRss
	{
		[XmlElement(ElementName = "channel")]
		public Channel Channel { get; set; }
		[XmlAttribute(AttributeName = "version")]
		public string Version { get; set; }
		[XmlAttribute(AttributeName = "dc", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Dc { get; set; }
		[XmlAttribute(AttributeName = "atom", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Atom { get; set; }
		[XmlAttribute(AttributeName = "media", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Media { get; set; }
		[XmlAttribute(AttributeName = "activity", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Activity { get; set; }
	}

}
