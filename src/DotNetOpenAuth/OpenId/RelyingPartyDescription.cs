﻿//-----------------------------------------------------------------------
// <copyright file="RelyingPartyDescription.cs" company="Andrew Arnott">
//     Copyright (c) Andrew Arnott. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DotNetOpenAuth.OpenId {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// A description of some OpenID Relying Party endpoint.
	/// </summary>
	/// <remarks>
	/// This is an immutable type.
	/// </remarks>
	internal class RelyingPartyEndpointDescription {
		/// <summary>
		/// Initializes a new instance of the <see cref="RelyingPartyEndpointDescription"/> class.
		/// </summary>
		/// <param name="returnTo">The return to.</param>
		/// <param name="supportedServiceTypeUris">
		/// The Type URIs of supported services advertised on a relying party's XRDS document.
		/// </param>
		internal RelyingPartyEndpointDescription(Uri returnTo, string[] supportedServiceTypeUris) {
			this.ReturnToEndpoint = returnTo;
			this.Protocol = GetProtocolFromServices(supportedServiceTypeUris);
		}

		/// <summary>
		/// Gets the URL to the login page on the discovered relying party web site.
		/// </summary>
		public Uri ReturnToEndpoint { get; private set; }

		/// <summary>
		/// Gets the OpenId protocol that the discovered relying party supports.
		/// </summary>
		public Protocol Protocol { get; private set; }

		private static Protocol GetProtocolFromServices(string[] supportedServiceTypeUris) {
			Protocol protocol = Protocol.FindBestVersion(p => p.RPReturnToTypeURI, supportedServiceTypeUris);
			if (protocol == null) {
				throw new InvalidOperationException("Unable to determine the version of OpenID the Relying Party supports.");
			}
			return protocol;
		}
	}
}
